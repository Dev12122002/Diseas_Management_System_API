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
    public class ReportsController : ControllerBase
    {
        private readonly DiseasContext _context;

        public ReportsController(DiseasContext context)
        {
            _context = context;
        }

        // GET: api/Reports
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        {
          if (_context.Reports == null)
          {
              return NotFound();
          }
            return await _context.Reports.ToListAsync();
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(int id)
        {
          if (_context.Reports == null)
          {
              return NotFound();
          }
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        [AllowAnonymous]
        [HttpGet("DiseasId={DiseasId}")]
        public async Task<ActionResult<IEnumerable<Report>>> GetBodyParts(int DiseasId)
        {
            List<Report> reports = new List<Report>();
            if (_context.DiseasBodyParts == null)
            {
                return NotFound();
            }

            foreach (var row in await _context.DiseasReports.Where(tbl => tbl.diseasId == DiseasId).ToListAsync())
            {
                Report? report = await _context.Reports.FindAsync(row.reportId);
                reports.Add(report);
            }

            return reports;
        }

        // PUT: api/Reports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport(int id, Report report)
        {
            if (id != report.reportId)
            {
                return BadRequest();
            }

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
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

        // POST: api/Reports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Report>> PostReport(Report report)
        {
          if (_context.Reports == null)
          {
              return Problem("Entity set 'DiseasContext.Reports'  is null.");
          }
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReport", new { id = report.reportId }, report);
        }

        // DELETE: api/Reports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            if (_context.Reports == null)
            {
                return NotFound();
            }
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportExists(int id)
        {
            return (_context.Reports?.Any(e => e.reportId == id)).GetValueOrDefault();
        }
    }
}
