using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DatabaseContext;
using WebAPI.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO.WebAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/labplan")]
    [ApiController]
    public class LabPlanController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        public LabPlanController(ProjectDbContext context)
        {
            _context = context;
        }

        // POST: api/LabPlan/Create
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateLabPlan([FromBody] LabPlanCreateDto labPlanDto)
        {
            // Check if the user is 'coco' (Assuming role checking is done by some middleware or service)
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            Console.WriteLine(userRole);
            if (userRole != "COCO")
            {
                return Forbid();
            }

            var course = await _context.courses.FindAsync(labPlanDto.CourseId);
            var subject = await _context.subjects.FindAsync(labPlanDto.SubjectId);
            var faculty = await _context.users.FindAsync(labPlanDto.FacultyId);

            if (course == null || subject == null || faculty == null)
            {
                return NotFound("Course, Subject, or Faculty not found.");
            }

            var labPlan = new LabPlan
            {
                CourseId = labPlanDto.CourseId,
                SubjectId = labPlanDto.SubjectId,
                FacultyId = labPlanDto.FacultyId,
                Course = course,
                Subject = subject,
                Faculty = faculty,
                ModuleName = labPlanDto.ModuleName,
                Group = labPlanDto.Group
            };

            _context.labplans.Add(labPlan);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLabPlanById), new { id = labPlan.Id }, labPlan);
        }

        // GET: api/LabPlan/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLabPlanById(int id)
        {
            var labPlan = await _context.labplans
                .Include(lp => lp.Course)
                .Include(lp => lp.Subject)
                .Include(lp => lp.Faculty)
                .FirstOrDefaultAsync(lp => lp.Id == id);

            if (labPlan == null)
            {
                return NotFound();
            }

            return Ok(labPlan);
        }

        // GET: api/LabPlan
        [HttpGet]
        public async Task<IActionResult> GetLabPlans()
        {
            var labPlans = await _context.labplans
                .Include(lp => lp.Course)
                .Include(lp => lp.Subject)
                .Include(lp => lp.Faculty)
                .ToListAsync();

            return Ok(labPlans);
        }
    }
}
