using System.Reflection.PortableExecutable;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Entities;

namespace API.Entities
{
    public class Request
    {
        public Request()
        {
            this.RequestTime = DateTime.Now;
            this.RequestStatus = "Open";
        }
        public Request(int CourseID, RequestDetails requestDetails, int? studentID = null, int? teacherID = null)
        {
            this.CourseID = CourseID;
            this.RequestDetails = requestDetails;
            this.StudentID = studentID;
            this.TeacherID = teacherID;
            this.RequestTime = DateTime.Now;
            this.RequestStatus = "Open";
        }
        public int RequestID { get; set; }

        public int CourseID { get; set; }
        public Course Course { get; set; }

        public int? ManagerId { get; set; }
        public Manager Manager  { get; set; }

        public int? StudentID { get; set; }
        public Student Student { get; set; }

        public int? TeacherID { get; set; }
        public Teacher Teacher { get; set; }
        
        [Required]
        public RequestDetails RequestDetails { get; set; }
        [Required]
        public DateTime RequestTime { get; set; }
        [Required]
        public string RequestStatus { get; set; }
    }

    public enum RequestDetails
    {
        ActiveCourse,
        DeleteCourse,
        RegisterStudentToCourse,
        RemoveStudentFromCourse
    }
}
