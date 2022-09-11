namespace API.Entities
{
    public class Unit
    {
        public int UnitID { get; set; }
        public string UnitName { get; set; }
        public string StudyContent { get; set; }
        public string Questions { get; set; }
        public int CourseID { get; set; }
        public Course Course { get; set; }
    }
}