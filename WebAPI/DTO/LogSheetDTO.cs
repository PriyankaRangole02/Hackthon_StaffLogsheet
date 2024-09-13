namespace WebAPI.DTO
{
    public class LogSheetDto
    {
        public int LogSheetId { get; set; }
        public int UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string LogType { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public string Topic { get; set; }
        public int VerifiedBy { get; set; }
        public int ApprovedBy { get; set; }
        public string Status { get; set; } // New property
    }
}
