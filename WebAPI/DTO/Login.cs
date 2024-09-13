using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO
{
    public class login
    {
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
