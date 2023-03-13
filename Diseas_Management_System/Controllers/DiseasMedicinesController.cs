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
    public class DiseasMedicinesController : ControllerBase
    {
        private readonly DiseasContext _context;

        public DiseasMedicinesController(DiseasContext context)
        {
            _context = context;
        }

        // GET: api/DiseasMedicines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiseasMedicine>>> GetDiseasMedicines()
        {
          if (_context.DiseasMedicines == null)
          {
              return NotFound();
          }
            return await _context.DiseasMedicines.ToListAsync();
        }

        // GET: api/DiseasMedicines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiseasMedicine>> GetDiseasMedicine(int id)
        {
          if (_context.DiseasMedicines == null)
          {
              return NotFound();
          }
            var diseasMedicine = await _context.DiseasMedicines.FindAsync(id);

            if (diseasMedicine == null)
            {
                return NotFound();
            }

            return diseasMedicine;
        }

        // PUT: api/DiseasMedicines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiseasMedicine(int id, DiseasMedicine diseasMedicine)
        {
            if (id != diseasMedicine.diseasMedicineId)
            {
                return BadRequest();
            }

            _context.Entry(diseasMedicine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiseasMedicineExists(id))
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

        // POST: api/DiseasMedicines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DiseasMedicine>> PostDiseasMedicine(DiseasMedicine diseasMedicine)
        {
          if (_context.DiseasMedicines == null)
          {
              return Problem("Entity set 'DiseasContext.DiseasMedicines'  is null.");
          }
            _context.DiseasMedicines.Add(diseasMedicine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiseasMedicine", new { id = diseasMedicine.diseasMedicineId }, diseasMedicine);
        }

        // DELETE: api/DiseasMedicines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiseasMedicine(int id)
        {
            if (_context.DiseasMedicines == null)
            {
                return NotFound();
            }
            var diseasMedicine = await _context.DiseasMedicines.FindAsync(id);
            if (diseasMedicine == null)
            {
                return NotFound();
            }

            _context.DiseasMedicines.Remove(diseasMedicine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiseasMedicineExists(int id)
        {
            return (_context.DiseasMedicines?.Any(e => e.diseasMedicineId == id)).GetValueOrDefault();
        }
    }
}
