using WebAPI.DatabaseContext;
using WebAPI.Model;

namespace WebAPI.Service
{
    public class UserRoleService
    {
        ProjectDbContext db = null;
        public UserRoleService() { }
        public UserRoleService(ProjectDbContext dbContext)
        {
            db = dbContext;
        }
        public bool Add(UserRole role)
        {
            db.Add(role);
            db.SaveChanges();
            return true;

        }
        public bool Delete(UserRole role)
        {
            db.Remove(role);
            db.SaveChanges();
            return true;
        }
        public bool Put(int id)
        {
            UserRole role = db.roles.Find(id);
            db.roles.Update(role);
            return true;

        }

        public  IEnumerable<UserRole> GetAllRoles()
        {
            return db.roles.ToList<UserRole>();
        }
    }
}

       
    




