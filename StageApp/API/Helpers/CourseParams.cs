using API.Helpers;

namespace API.Helpers
{
    public class CourseParams : PaginationParams
    {
        public string Role { get; set; }

        public string Name { get; set; }   
        
    }
}