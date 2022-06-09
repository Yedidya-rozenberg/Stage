using System.Security.Cryptography.X509Certificates;
namespace API.Entities
{

    public class Interest
    {
        public int InterestId { get; set; }
        
        
        public InterestName Name { get; set; }

        public Student Student { get; set; }

    }

        public enum InterestName
    {
    Programming, PersonalDevelopment, LifeSkills, Science, Languages, Office, Humanities
    }  
  
}