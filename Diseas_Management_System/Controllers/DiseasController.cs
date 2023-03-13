using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Diseas_Management_System.Models;
using Microsoft.AspNetCore.Authorization;

namespace Diseas_Management_System.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DiseasController : ControllerBase
    {
        private readonly DiseasContext _context;

        public DiseasController(DiseasContext context)
        {
            _context = context;
        }

        // GET: api/Diseas
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diseas>>> GetDiseases()
        {
          if (_context.Diseases == null)
          {
              return NotFound();
          }
            return await _context.Diseases.ToListAsync();
        }

        // GET: api/Diseas/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Diseas>> GetDiseas(int id)
        {
          if (_context.Diseases == null)
          {
              return NotFound();
          }
            var diseas = await _context.Diseases.FindAsync(id);

            if (diseas == null)
            {
                return NotFound();
            }

            return diseas;
        }

        // PUT: api/Diseas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiseas(int id, Diseas diseas)
        {
            if (id != diseas.diseasId)
            {
                return BadRequest();
            }

            _context.Entry(diseas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiseasExists(id))
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

        // POST: api/Diseas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Diseas>> PostDiseas(Diseas diseas)
        {
          if (_context.Diseases == null)
          {
              return Problem("Entity set 'DiseasContext.Diseases'  is null.");
          }
            _context.Diseases.Add(diseas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiseas", new { id = diseas.diseasId }, diseas);
        }

        // DELETE: api/Diseas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiseas(int id)
        {
            if (_context.Diseases == null)
            {
                return NotFound();
            }
            var diseas = await _context.Diseases.FindAsync(id);
            if (diseas == null)
            {
                return NotFound();
            }

            _context.Diseases.Remove(diseas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiseasExists(int id)
        {
            return (_context.Diseases?.Any(e => e.diseasId == id)).GetValueOrDefault();
        }
    }
}
