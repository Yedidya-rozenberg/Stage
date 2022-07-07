namespace API.DTOs
{
    public class CreateUnirDto
    {
        public int CourseID { get; set; }
        public string UnitName { get; set; }

        public string StudyContent { get; set; }
        public string Questions { get; set; }
    }
}