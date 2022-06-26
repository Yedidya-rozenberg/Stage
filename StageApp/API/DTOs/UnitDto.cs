namespace API.DTOs
{
    public class UnitDto
    {
        public int UnitID { get; set; }

        public int CourseID { get; set; }
        public string UnitName { get; set; }

        public string StudyContent { get; set; }
        public string Questions { get; set; }
    }
}