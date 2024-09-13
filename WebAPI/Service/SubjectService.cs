using System.Linq;
using WebAPI.DatabaseContext;
using WebAPI.Model;

namespace WebAPI.Service
{
    public class SubjectService
    {
        ProjectDbContext db = null;
        public SubjectService(ProjectDbContext dbContext) {
            db= dbContext;


        }
        public SubjectService()
        {
            
        }
        public bool Add(Subject sub)
        {
            db.Add(sub);
            db.SaveChanges();
            return true;

        }
        public bool Delete(int id)
        {
            Subject sub = db.subjects.Find(id);
            db.Remove(sub);
            db.SaveChanges();
            return true;
        }
        public bool Put(int id)
        {
            Subject sub = db.subjects.Find(id);
            db.subjects.Update(sub);
            return true;

        }

        public IEnumerable<Subject> GetAllSubjects()
        {
            return db.subjects.ToList<Subject>();
        }

        public Subject FindById(int id)
        {
            Subject sub=db.subjects.Find(id);
            return sub;
        }
    }
}
