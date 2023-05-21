using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Psinder.Api.Data;
using Psinder.Api.Models;
using Psinder.Api.Services;

namespace Psinder.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPetService _petService;

        public PetsController(
            IWebHostEnvironment webHostEnvironment,
            IPetService petService)
        {
            _webHostEnvironment = webHostEnvironment;
            _petService = petService;
        }

        // GET: api/Pets
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PetModel>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<PetModel>>> GetAllPets()
        {
            var pets = await _petService.GetAllPets();
            if (pets == null || pets.Count == 0)
            {
                return NoContent();
            }
            return Ok(pets);
        }
        [HttpGet]
        public async Task<ActionResult<List<PetModel>>> SearchPetByName(string name)
        {
            var pet = await _petService.SearchPetByName(name);
            return Ok(pet);
        }

        // GET: api/Pets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PetModel>> GetPet(int id)
        {
            var pet = await _petService.GetPet(id);
            if (pet == null)
            {
                return NotFound();
            }
            return Ok(pet);            
        }
        
        // PUT: api/Pets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<PetModel>> PutPet(int id, PetModel pet)
        {
            if (id != pet.PetId)
            {
                return BadRequest();
            }

            try
            {
                return await _petService.UpdatePet(pet);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Błąd w edycji schroniska");
            }
        }

        // POST: api/Pets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet(PetModel pet)
        {
            try
            {
                if (pet == null)
                {
                    return BadRequest();
                }
                var result = await _petService.AddPet(pet);

                return CreatedAtAction(nameof(GetPet), new { id = result.PetId }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Błąd przy tworzeniu profilu zwierzaka");
            }
        }

        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pet>> DeletePet(int id)
        {
            try
            {
                var employeeToDelete = await _petService.GetPet(id);

                if (employeeToDelete == null)
                {
                    return NotFound($"Nie znaleziono schroniska o id {id} ");
                }

                return await _petService.DeletePet(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Bład przy usuwaniu schroniska");
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
