using System.Collections;
using System.Collections.Generic;

namespace API.Entities
{
    public class Student : AppUser
    {
        public float Payment { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}