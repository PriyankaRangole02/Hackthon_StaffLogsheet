using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Model
{
    public class LabPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }

        [ForeignKey("Faculty")]
        public int FacultyId { get; set; }  // Represents the faculty member assigned to the lab plan

        public Course Course { get; set; }
        public Subject Subject { get; set; }
        public User Faculty { get; set; }  // Represents the faculty member assigned to the lab plan

        public string ModuleName { get; set; }  // Name of the module (e.g., 'DBT')
        public GroupName Group { get; set; }    // Enum for group names
    }
    public enum GroupName
    {
        KD1,
        KD2,
        KD3,
        KD4
    }

}
