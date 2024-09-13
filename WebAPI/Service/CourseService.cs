using WebAPI.DatabaseContext;
using WebAPI.Model;

namespace WebAPI.Service
{
    public class CourseService
    {
        ProjectDbContext db = null;

        public CourseService() { }
        public CourseService(ProjectDbContext dbContext)
        {
            db = dbContext;
        }
        public bool AddCourse(Course course)
        {
            db.Add(course);
            db.SaveChanges();
            return true;
        }
        public bool DeleteCourse(int courseId)
        {
            db.Remove(courseId);
            db.SaveChanges();
            return true;
        }
        public bool UpdateCourse(int courseId)
        {
            Course couId = db.courses.Find(courseId);
            db.Update(couId);
            db.SaveChanges();
            return true;
        }
        public IEnumerable<Course> GetAllCourse() {
            return db.courses.ToList<Course>();
        }
    }
}
