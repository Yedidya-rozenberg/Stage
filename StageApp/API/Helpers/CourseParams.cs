namespace API.Helpers
{
    public class CourseParams : PaginationParams
    {
        public bool MyCourses { get; set; } = false;
        public string CurrentUser { get; set; }
        public string TeacherName { get; set; }
    }
}