using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Model
{
    public class CourseSubject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int CourseSubjectId { get; set; }
        [ForeignKey("GetCourse")]
        public int CourseId { get; set; }
       
        [ForeignKey("GetSubject")]
        public int SubjectId { get; set; }

        public Subject   GetSubject { get; set; }
        public Course GetCourse { get; set; }

    }
}
