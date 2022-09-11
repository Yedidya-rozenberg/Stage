namespace API.DTOs
{
    public class CourseDto
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public bool CourseStatus { get; set; }
        public string PhotoUrl { get; set; }
        public int TeacherID { get; set; }
        public string TeacherName { get; set; }
    }
}