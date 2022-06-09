using System.Collections.Generic;

namespace API.Entities
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public bool CourseStatus { get; set; }
        public Photo Photo { get; set; }

        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Unit> Units { get; set; }
    }
}