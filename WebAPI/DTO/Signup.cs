using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO
{
    public class UserListDTO


    {

        public int UserId { get; set; }
        public string? Email { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }


        public string? MobileNo { get; set; }


        public int RoleId { get; set; }


    }
}

