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
        IPetService petService) : ControllerBase
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
        [HttpGet]
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
        public async Task<ActionResult<PetDto>> PutPet(int id, PetDto pet)
        {
            if (id != pet.Id)
            {
                return BadRequest();
            }

            try
            {
                return await _petService.UpdatePet(pet);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while altering pet's data" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet(PetDto pet)
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

                return await _petService.DeletePet(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while deleting a pet" + ex.Message);
            }
        }
        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }
    }
}

