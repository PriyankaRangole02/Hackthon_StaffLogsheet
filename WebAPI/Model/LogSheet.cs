using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.Identity.Client;

namespace WebAPI.Model
{
    public class LogSheet
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogSheetId { get; set; }
        [ForeignKey("GetUser")]
        public int UserId { get; set; }
        public DateTime StartTime { get; set; }


        public DateTime EndTime { get; set; }
        public string LogType { get; set; }
        [ForeignKey("GetCourse")]
        public int CourseId { get; set; }

        [ForeignKey("GetSubject")]
        public int SubjectId { get; set; }

        public string Topic { get; set; }
        public string Status { get; set; }


        public int? verifiedBy { get; set; }

        public int? ApprovedBy { get; set; }

        public User GetUser { get; set; }
        public Subject GetSubject { get; set; }
        public Course GetCourse { get; set; }



        public LogSheet()
        {

        }

        public LogSheet(int verifiedBy, int ApprovedBy)
        {
            this.verifiedBy = verifiedBy;
            this.ApprovedBy = ApprovedBy;
        }



        public override string ToString()
        {
            return $"UserId: {UserId}";


        }






    }
}
