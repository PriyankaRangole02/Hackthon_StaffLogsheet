using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model
{
    public class UserRole { 


         [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int RoleId { get; set; }
       public string RoleName { get; set; }
    }
}
