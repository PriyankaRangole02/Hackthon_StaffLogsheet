using WebAPI.Model;

namespace WebAPI.DTO
{
    
    

    namespace WebAPI.DTO
    {
        public class LabPlanCreateDto
        {
            public int CourseId { get; set; }
            public int SubjectId { get; set; }
            public int FacultyId { get; set; }
            public string ModuleName { get; set; }
            public GroupName Group { get; set; }
        }
    }

}
