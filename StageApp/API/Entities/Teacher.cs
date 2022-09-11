using System.Collections.Generic;

namespace API.Entities
{
    public class Teacher : AppUser
    {
        public float Salary { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}