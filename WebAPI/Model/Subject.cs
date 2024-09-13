using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Model
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int TheoryHrs { get; set; }
        public int LabHrs { get; set; }
    }
}
