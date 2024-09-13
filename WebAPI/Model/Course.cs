using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Model
{
    
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Duration { get; set; }
    }
}
