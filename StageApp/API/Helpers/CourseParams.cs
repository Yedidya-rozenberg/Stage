using API.Helpers;

namespace API.Helpers
{
    public class CourseParams : PaginationParams
    {
        public bool MyCourses { get; set; } = false;
        
        public string Role { get; set; }

        public string CurrentUser { get; set; }

        public string TeacherName { get; set; }
        
    }
}