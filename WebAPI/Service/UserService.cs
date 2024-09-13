using WebAPI.DatabaseContext;
using WebAPI.Model;

namespace WebAPI.Service
{
    public class UserService
    {
        ProjectDbContext db=null;
        public UserService()
        {
            
        }
        public UserService(ProjectDbContext dbContext)
        {
            db = dbContext;


        }
        public bool Add(User user)
        {
            db.users.Add(user);
            db.SaveChanges();
            return true;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return db.users.ToList<User>();
        }

    }

    
}
