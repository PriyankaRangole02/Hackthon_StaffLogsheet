using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAPI.Model;

namespace WebAPI.DTO
{
    public class UserDTO
    {
        
        
        public string? Email { get; set; }
       
        public string? UserName { get; set; }
        
        public string? Password { get; set; }

        
        public string? MobileNo { get; set; }

        
        public int RoleId { get; set; }
        
    }
}
