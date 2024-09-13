using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DatabaseContext;
using WebAPI.DTO;
using WebAPI.Model;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [Route("/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        CourseService service;
        ProjectDbContext db;

        public CourseController(ProjectDbContext dbContext)
        {
            db = dbContext;
            service = new CourseService();
        }
        [Authorize]
        [HttpGet]
        public IEnumerable<GetCourseDTO> Get()
        {
            List<GetCourseDTO> CourseDTOList = new List<GetCourseDTO>();
            foreach (Course course in db.courses.ToList<Course>())
            {
                GetCourseDTO edl = new GetCourseDTO();
                edl.courseId = course.CourseId;
                edl.CourseName = course.CourseName;
                edl.Duration = course.Duration;

                CourseDTOList.Add(edl);


            }
            return CourseDTOList;



        }

        [HttpPost]
        public void post([FromBody] AddCourseDTO coursetDTO)
        {
            Course course = new Course();
            course.CourseName= coursetDTO.CourseName;
            course.Duration= coursetDTO.Duration;
            service.AddCourse(course);
            db.SaveChanges();
           
  
        }
        [HttpPut("id")]
        public void put(int id, [FromBody] SubjectDTO subDTO)

        {
            service.UpdateCourse(id);


        }
        [HttpDelete("id")]
        public bool remove(int id)
        {
            //Subject sub = service.findById(id);
            service.DeleteCourse(id);
            return true;
        }
    }
}
