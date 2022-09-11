using API.Helpers;
using System.Threading.Tasks;
using API.Entities;
using System.Collections.Generic;
using API.DTOs;
namespace API.Interfaces
{
    public interface ICourseRepository
    {
        Task<PageList<CourseDto>> GetCoursesAsync(CourseParams courseParams);
        void AddCourse(Course course);
        void Update(Course course);
        Task<Course> GetCourseByIdAsync(int id);
        Task<CourseDto> GetCourseByNameAsync(string name);
        void DisableCourse(Course course);
        Task<IEnumerable<MemberDto>> GetStudentsByCourseIdAsync(int id);
        void RegisterStudentToCourse(int courseId, Student student);
        void UnregisterStudentFromCourse(int courseId, Student student);
        Task<bool> CheckStudentCourse(int courseId, int studentId);
    }
}