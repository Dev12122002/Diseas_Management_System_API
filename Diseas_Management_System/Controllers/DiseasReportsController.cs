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
    public class DiseasReportsController : ControllerBase
    {
        private readonly DiseasContext _context;

        public DiseasReportsController(DiseasContext context)
        {
            _context = context;
        }

        // GET: api/DiseasReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiseasReport>>> GetDiseasReports()
        {
          if (_context.DiseasReports == null)
          {
              return NotFound();
          }
            return await _context.DiseasReports.ToListAsync();
        }

        // GET: api/DiseasReports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiseasReport>> GetDiseasReport(int id)
        {
            var diseasReport = await _context.DiseasReports.FindAsync(id);

            if (diseasReport == null)
            {
                return NotFound();
            }

            return diseasReport;
        }

        // PUT: api/DiseasReports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiseasReport(int id, DiseasReport diseasReport)
        {
            if (id != diseasReport.diseasReportId)
            {
                return BadRequest();
            }

            _context.Entry(diseasReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiseasReportExists(id))
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

        // POST: api/DiseasReports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DiseasReport>> PostDiseasReport(DiseasReport diseasReport)
        {
          if (_context.DiseasReports == null)
          {
              return Problem("Entity set 'DiseasContext.DiseasReports'  is null.");
          }
            _context.DiseasReports.Add(diseasReport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiseasReport", new { id = diseasReport.diseasReportId }, diseasReport);
        }

        [HttpDelete("DiseasId={DiseasId}")]
        public async Task<IActionResult> DeleteDiseasReportsByDiseasId(int DiseasId)
        {
            if (_context.DiseasBodyParts == null)
            {
                return NotFound();
            }

            foreach (var row in await _context.DiseasReports.Where(tbl => tbl.diseasId == DiseasId).ToListAsync())
            {
                _context.DiseasReports.Remove(row);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/DiseasReports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiseasReport(int id)
        {
            if (_context.DiseasReports == null)
            {
                return NotFound();
            }
            var diseasReport = await _context.DiseasReports.FindAsync(id);
            if (diseasReport == null)
            {
                return NotFound();
            }

            _context.DiseasReports.Remove(diseasReport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiseasReportExists(int id)
        {
            return (_context.DiseasReports?.Any(e => e.diseasReportId == id)).GetValueOrDefault();
        }
    }
}
