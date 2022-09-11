namespace API.DTOs
{
    public class CreateUnitDto
    {
        public int CourseID { get; set; }
        public string UnitName { get; set; }

        public string StudyContent { get; set; }
        public string Questions { get; set; }

        public int? NextUnitId { get; set; }
        public int? BackUnitId { get; set; }
    }
}