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
    public class DiseasBodyPartsController : ControllerBase
    {
        private readonly DiseasContext _context;

        public DiseasBodyPartsController(DiseasContext context)
        {
            _context = context;
        }

        // GET: api/DiseasBodyParts
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiseasBodyPart>>> GetDiseasBodyParts()
        {
          if (_context.DiseasBodyParts == null)
          {
              return NotFound();
          }
            return await _context.DiseasBodyParts.ToListAsync();
        }

        //[AllowAnonymous]
        //[HttpGet("BodyParts/{DiseasId}")]
        //public async Task<ActionResult<IEnumerable<BodyPart>>> GetBodyParts(int DiseasId)
        //{
        //    List<BodyPart> bodyparts = new List<BodyPart>();
        //    if (_context.DiseasBodyParts == null)
        //    {
        //        return NotFound();
        //    }
            
        //    foreach (var row in await _context.DiseasBodyParts.Where(tbl => tbl.diseasId == DiseasId).ToListAsync())
        //    {
        //        BodyPart? bodypart = await _context.BodyParts.FindAsync(row.bodypartId);
        //        bodyparts.Add(bodypart);
        //    }

        //    return bodyparts;
        //}

        // GET: api/DiseasBodyParts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiseasBodyPart>> GetDiseasBodyPart(int id)
        {
            var diseasBodyPart = await _context.DiseasBodyParts.FindAsync(id);

            if (diseasBodyPart == null)
            {
                return NotFound();
            }

            return diseasBodyPart;
        }

        // PUT: api/DiseasBodyParts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiseasBodyPart(int id, DiseasBodyPart diseasBodyPart)
        {
            if (id != diseasBodyPart.diseasBodyPartId)
            {
                return BadRequest();
            }

            _context.Entry(diseasBodyPart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiseasBodyPartExists(id))
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

        // POST: api/DiseasBodyParts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DiseasBodyPart>> PostDiseasBodyPart(DiseasBodyPart diseasBodyPart)
        {
          if (_context.DiseasBodyParts == null)
          {
              return Problem("Entity set 'DiseasContext.DiseasBodyParts'  is null.");
          }
            _context.DiseasBodyParts.Add(diseasBodyPart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiseasBodyPart", new { id = diseasBodyPart.diseasBodyPartId }, diseasBodyPart);
        }

        // DELETE: api/DiseasBodyParts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiseasBodyPart(int id)
        {
            if (_context.DiseasBodyParts == null)
            {
                return NotFound();
            }
            var diseasBodyPart = await _context.DiseasBodyParts.FindAsync(id);
            if (diseasBodyPart == null)
            {
                return NotFound();
            }

            _context.DiseasBodyParts.Remove(diseasBodyPart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DiseasId={DiseasId}")]
        public async Task<IActionResult> DeleteDiseasBodyPartsByDiseasId(int DiseasId)
        {
            if (_context.DiseasBodyParts == null)
            {
                return NotFound();
            }

            foreach (var row in await _context.DiseasBodyParts.Where(tbl => tbl.diseasId == DiseasId).ToListAsync())
            {
                _context.DiseasBodyParts.Remove(row);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiseasBodyPartExists(int id)
        {
            return (_context.DiseasBodyParts?.Any(e => e.diseasBodyPartId == id)).GetValueOrDefault();
        }
    }
}
