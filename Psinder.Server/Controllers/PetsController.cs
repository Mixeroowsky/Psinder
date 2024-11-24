using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Psinder.Server.Dtos;
using Psinder.Server.Entities;
using Psinder.Server.Services;

namespace Psinder.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PetsController(
        IWebHostEnvironment webHostEnvironment,
        IPetService petService,
        IFileService fileService) : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        private readonly IPetService _petService = petService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PetDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<PetDto>>> GetAllPets()
        {
            var pets = await _petService.GetAllPets();
            if (pets == null || pets.Count == 0)
            {
                return NoContent();
            }
            return Ok(pets);
        }
        [HttpGet("{name}")]
        public async Task<ActionResult<List<PetDto>>> SearchPetByName(string name)
        {
            var pet = await _petService.SearchPetByName(name);
            return Ok(pet);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetDto>> GetPet(int id)
        {
            var pet = await _petService.GetPet(id);
            if (pet == null)
            {
                return NotFound();
            }
            return Ok(pet);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PetDto>> PutPet(int id, [FromForm] PetDto pet)
        {
            if (id != pet.Id)
            {
                return BadRequest("Id in URL and form does not match");
            }
            var existingPet = await _petService.GetPet(id);
            if (existingPet == null)
            {
                return BadRequest($"Pet with id {id} not found");
            }
            string oldImage = existingPet.PhotoUrl;
            if (pet.ImageFile != null)
            {
                if (pet.ImageFile?.Length > 1 * 5120 * 5120)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 5 MB");
                }
                string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
                string createdImageName = await fileService.SaveFileAsync(pet.ImageFile, allowedFileExtentions);
                pet.PhotoUrl = createdImageName;
            }
            if (existingPet.ImageFile != null)
            {
                fileService.DeleteFile(oldImage);
            }
            try
            {
                var updatedPet = await _petService.UpdatePet(pet);
                return Ok(updatedPet);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while altering pet's data" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PetDto>> PostPet([FromForm] PetDto pet)
        {
            try
            {
                if (pet == null)
                {
                    return BadRequest();
                }
                var shelterId = await _petService.GetPetsShelter(pet.ShelterId);
                if (shelterId == null)
                {
                    return BadRequest("Shelter missing for this pet");
                }
                if (pet.ImageFile?.Length > 1 * 5120 * 5120)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 5 MB");
                }
                string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
                string createdImageName = await fileService.SaveFileAsync(pet.ImageFile, allowedFileExtentions);
                pet.PhotoUrl = createdImageName;
                var result = await _petService.AddPet(pet);

                return CreatedAtAction(nameof(GetPet), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while creating new pet" + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pet>> DeletePet(int id)
        {
            try
            {
                var petToDelete = await _petService.GetPet(id);

                if (petToDelete == null)
                {
                    return NotFound($"Pet not found by id: {id}");
                }

                await _petService.DeletePet(id);
                fileService.DeleteFile(petToDelete.PhotoUrl);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while deleting a pet" + ex.Message);
            }
        }
    }    
}

