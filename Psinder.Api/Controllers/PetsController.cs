using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<PetModel>> GetPetById(int id)
        {
            var pet = await _petService.GetPetById(id);
            if (pet == null)
            {
                return NotFound();
            }
            return Ok(pet);            
        }
        /*
        // PUT: api/Pets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet(int id, Pet pet)
        {
            if (id != pet.PetId)
            {
                return BadRequest();
            }

            _context.Entry(pet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet(Pet pet)
        {            
            if (_context.Pets == null)
            {
                return Problem("Entity set 'PsinderContext.Pets' is null.");
            }
            if (pet.PhotoUrl != null)
            {
                string folder = "books/cover/";
                //pet.PhotoUrl = await UploadImage(folder, pet.PhotoFile);
            }
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPet", new { id = pet.PetId }, pet);
        }

        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            if (_context.Pets == null)
            {
                return NotFound();
            }
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }
        private bool PetExists(int id)
        {
            return (_context.Pets?.Any(e => e.PetId == id)).GetValueOrDefault();
        }*/
    }
}
