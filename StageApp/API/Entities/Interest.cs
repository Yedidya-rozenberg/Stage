using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{

    public class Interest
    {
        public int InterestId { get; set; }
        
        public InterestName Name { get; set; }

        public IEnumerable<Student> Students { get; set; }

        public IEnumerable<Course> Courses { get; set; }

    }


        public enum InterestName
    {
        Art,
        Humanities,
        Science,
        SocialScience,
        Technology,
        Other,
        Programming,
        Gaming,
        Music
    }  
  
}