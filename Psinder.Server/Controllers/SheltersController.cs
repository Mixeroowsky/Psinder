﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Psinder.Server.Dtos;
using Psinder.Server.Entities;
using Psinder.Server.Services;


namespace Psinder.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SheltersController(IShelterService shelterService) : ControllerBase
    {
        private readonly IShelterService _shelterService = shelterService;

        [HttpGet]
        public async Task<ActionResult<List<ShelterDto>>> GetAllShelters()
        {
            var shelters = await _shelterService.GetAllShelters();
            if (shelters == null || shelters.Count == 0)
            {
                return NoContent();
            }
            return Ok(shelters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShelterDto>> GetShelterById(int id)
        {
            var shelter = await _shelterService.GetShelterById(id);
            if (shelter == null)
            {
                return NotFound();
            }
            return shelter;
        }

        [HttpPost]
        public async Task<ActionResult<ShelterDto>> AddShelter([FromBody] ShelterDto shelter)
        {
            try
            {
                if (shelter == null)
                {
                    return BadRequest();
                }
                var existingShelter = await _shelterService.GetShelterByName(shelter.Name);
                if (existingShelter != null)
                {
                    return BadRequest("Shelter by this name already exists");
                }
                var result = await _shelterService.AddShelter(shelter);

                return CreatedAtAction(nameof(GetShelterById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while creating a shelter: " + ex.InnerException);
            }
        }        
        [HttpPut("{id}")]
        public async Task<ActionResult<ShelterDto>> UpdateShelter(int id, [FromBody] ShelterDto shelter)
        {            
            shelter.Id = id;
            try
            {
                return await _shelterService.UpdateShelter(shelter);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while altering shelters data" + ex.InnerException);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Shelter>> DeleteShelter(int id)
        {
            try
            {
                var shelterToDelete = await _shelterService.GetShelterById(id);

                if (shelterToDelete == null)
                {
                    return NotFound($"Shelter missing by id: {id}");
                }

                return await _shelterService.DeleteShelter(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while deleting a shelter" + ex.Message + ex.InnerException);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<int>> ShelterByUserId(int id)
        {
            var shelter = await _shelterService.GetShelterByUser(id);
            if (shelter == null)
            {
                return NotFound();
            }
            int shelterId = shelter.Id;
            return shelterId;
        }
    }
}
