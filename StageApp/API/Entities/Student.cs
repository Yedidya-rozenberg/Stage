using System.Collections;
using System.Collections.Generic;

namespace API.Entities
{
    public class Student : AppUser
    {
        public float Payment { get; set; }
        public IEnumerable<Interest> Interests { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}