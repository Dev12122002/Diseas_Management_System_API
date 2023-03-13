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
    public class BodyPartsController : ControllerBase
    {
        private readonly DiseasContext _context;

        public BodyPartsController(DiseasContext context)
        {
            _context = context;
        }

        // GET: api/BodyParts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BodyPart>>> GetBodyParts()
        {
          if (_context.BodyParts == null)
          {
              return NotFound();
          }
            return await _context.BodyParts.ToListAsync();
        }

        // GET: api/BodyParts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BodyPart>> GetBodyPart(int id)
        {
          if (_context.BodyParts == null)
          {
              return NotFound();
          }
            var bodyPart = await _context.BodyParts.FindAsync(id);

            if (bodyPart == null)
            {
                return NotFound();
            }

            return bodyPart;
        }

        // PUT: api/BodyParts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBodyPart(int id, BodyPart bodyPart)
        {
            if (id != bodyPart.bodypartId)
            {
                return BadRequest();
            }

            _context.Entry(bodyPart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BodyPartExists(id))
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

        // POST: api/BodyParts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BodyPart>> PostBodyPart(BodyPart bodyPart)
        {
          if (_context.BodyParts == null)
          {
              return Problem("Entity set 'DiseasContext.BodyParts'  is null.");
          }
            _context.BodyParts.Add(bodyPart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBodyPart", new { id = bodyPart.bodypartId }, bodyPart);
        }

        // DELETE: api/BodyParts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBodyPart(int id)
        {
            if (_context.BodyParts == null)
            {
                return NotFound();
            }
            var bodyPart = await _context.BodyParts.FindAsync(id);
            if (bodyPart == null)
            {
                return NotFound();
            }

            _context.BodyParts.Remove(bodyPart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BodyPartExists(int id)
        {
            return (_context.BodyParts?.Any(e => e.bodypartId == id)).GetValueOrDefault();
        }
    }
}
