using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Model;
using WebAPI.DTO;
using WebAPI.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogSheetController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        public LogSheetController(ProjectDbContext context)
        {
            _context = context;
        }

       

    [HttpPost("create")]
    public async Task<IActionResult> CreateLogSheet([FromBody] AddLogSheet logSheetDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Map the DTO to the LogSheet entity
        var logSheet = new LogSheet
        {
            UserId = logSheetDto.UserId,
            StartTime = logSheetDto.StartTime,
            EndTime = logSheetDto.EndTime,
            LogType = logSheetDto.LogType,
            CourseId = logSheetDto.CourseId,
            SubjectId = logSheetDto.SubjectId,
            Topic = logSheetDto.Topic
        };

        try
        {
            // Add the new log sheet to the database
            await _context.LogSheets.AddAsync(logSheet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLogSheetById), new { id = logSheet.LogSheetId }, logSheet);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLogSheetById(int id)
        {
            var logSheet = await _context.LogSheets.FindAsync(id);
            if (logSheet == null)
            {
                return NotFound();
            }
            return Ok(logSheet);
        }

        // POST: api/LogSheet/Verify/{id}
        [HttpPost("Verify/{id}")]
        public IActionResult VerifyLogSheet(int id, [FromBody] int verifierId)
        {
            var logSheet = _context.LogSheets.Find(id);

            if (logSheet == null)
            {
                return NotFound();
            }

            logSheet.verifiedBy = verifierId;
            logSheet.Status = "Verified";

            _context.SaveChanges();

            return NoContent();
        }

        // POST: api/LogSheet/Approve/{id}
        [HttpPost("Approve/{id}")]
        public IActionResult ApproveLogSheet(int id, [FromBody] int approverId)
        {
            var logSheet = _context.LogSheets.Find(id);

            if (logSheet == null)
            {
                return NotFound();
            }

            if (logSheet.Status != "Verified")
            {
                return BadRequest("Log sheet must be verified before approval.");
            }

            logSheet.ApprovedBy = approverId;
            logSheet.Status = "Approved";

            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet]
        public IActionResult GetLogSheets()
        {
            try
            {
                var logSheets = _context.LogSheets
                    .Include(ls => ls.GetUser)
                    .Include(ls => ls.GetCourse)
                    .Include(ls => ls.GetSubject)
                    .ToList();

                if (logSheets == null || !logSheets.Any())
                {
                    return NotFound("No log sheets found.");
                }

                return Ok(logSheets);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

