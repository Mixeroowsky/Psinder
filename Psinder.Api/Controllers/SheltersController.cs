using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Psinder.Api.Data;

namespace Psinder.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SheltersController : ControllerBase
    {
        private readonly PsinderContext _context;

        public SheltersController(PsinderContext context)
        {
            _context = context;
        }

        // GET: api/Shelters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shelter>>> GetShelters()
        {
          if (_context.Shelters == null)
          {
              return NotFound();
          }
            return await _context.Shelters.ToListAsync();
        }

        // GET: api/Shelters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shelter>> GetShelter(int id)
        {
          if (_context.Shelters == null)
          {
              return NotFound();
          }
            var shelter = await _context.Shelters.FindAsync(id);

            if (shelter == null)
            {
                return NotFound();
            }

            return shelter;
        }

        // PUT: api/Shelters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShelter(int id, Shelter shelter)
        {
            if (id != shelter.ShelterId)
            {
                return BadRequest();
            }

            _context.Entry(shelter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShelterExists(id))
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

        // POST: api/Shelters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shelter>> PostShelter(Shelter shelter)
        {
          if (_context.Shelters == null)
          {
              return Problem("Entity set 'PsinderContext.Shelters'  is null.");
          }
            _context.Shelters.Add(shelter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShelter", new { id = shelter.ShelterId }, shelter);
        }

        // DELETE: api/Shelters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShelter(int id)
        {
            if (_context.Shelters == null)
            {
                return NotFound();
            }
            var shelter = await _context.Shelters.FindAsync(id);
            if (shelter == null)
            {
                return NotFound();
            }

            _context.Shelters.Remove(shelter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShelterExists(int id)
        {
            return (_context.Shelters?.Any(e => e.ShelterId == id)).GetValueOrDefault();
        }
    }
}
