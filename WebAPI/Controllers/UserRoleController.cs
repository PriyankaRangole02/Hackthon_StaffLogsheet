using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DatabaseContext;
using WebAPI.DTO;
using WebAPI.Model;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [Route("/userrole")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        
        private ProjectDbContext db;
        public UserRoleController(ProjectDbContext _db) {
            db=_db;

            


        }

        [HttpGet]
          public IEnumerable<RoleDTO> Get()
          {
            List<RoleDTO> RoleDTOList = new List<RoleDTO>();
            foreach (UserRole urole in db.roles.ToList<UserRole>() )
            {
               RoleDTO edl = new RoleDTO();
                urole.RoleName = urole.RoleName;

                RoleDTOList.Add(edl);


            }
            return RoleDTOList;
          
            
          }

        [HttpPut("id")]
        public bool put(int id,[FromBody]RoleDTO roleDTO)

        {
            UserRole role = db.roles.Find(id);

            db.roles.Update(role);
            return true;


        }

    }
}
