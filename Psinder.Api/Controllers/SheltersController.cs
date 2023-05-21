using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Psinder.Api.Data;
using Psinder.Api.Models;
using Psinder.Api.Services;

namespace Psinder.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SheltersController : ControllerBase
    {
        private readonly IShelterService _shelterService;

        public SheltersController(IShelterService shelterService)
        {
            _shelterService = shelterService;
        }

        // GET: api/Shelters
        [HttpGet]
        public async Task<ActionResult<List<ShelterModel>>> GetAllShelters()
        {
            var shelters = await _shelterService.GetAllShelters();
            if (shelters == null || shelters.Count == 0)
            {
                return NoContent();
            }
            return Ok(shelters);
        }

        // GET: api/Shelters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShelterModel>> GetShelter(int id)
        {
            var shelter = await _shelterService.GetShelterById(id);
            if (shelter == null)
            {
                return NotFound();
            }
            return shelter;
        }

        // PUT: api/Shelters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ShelterModel>> PutShelter(int id, ShelterModel shelter)
        {            
            if (id != shelter.ShelterId)
            {
                return BadRequest();
            }

            try
            {
                return await _shelterService.UpdateShelter(shelter);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Błąd w edycji schroniska");
            }
        }

        // POST: api/Shelters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShelterModel>> PostShelter(ShelterModel shelter)
        {            
            try
            {
                if (shelter == null)
                {
                    return BadRequest();
                }
                var result = await _shelterService.AddShelter(shelter);

                return CreatedAtAction(nameof(GetShelter), new { id = result.ShelterId }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Błąd przy tworzeniu schroniska");
            }            
        }

        // DELETE: api/Shelters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Shelter>> DeleteShelter(int id)
        {
            try
            {
                var employeeToDelete = await _shelterService.GetShelterById(id);

                if (employeeToDelete == null)
                {
                    return NotFound($"Nie znaleziono schroniska o id {id} ");
                }

                return await _shelterService.DeleteShelter(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Bład przy usuwaniu schroniska");
            }
        }
    }
}
