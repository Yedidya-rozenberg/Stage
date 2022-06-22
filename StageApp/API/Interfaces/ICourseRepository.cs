using System.Reflection.Metadata;
using System;
using API.Helpers;
using System.Threading.Tasks;
using API.Entities;
using System.Collections.Generic;
using API.DTOs;

namespace API.Interfaces

{
    public interface ICourseRepository
    {
        Task<PageList<CourseDto>> GetCoursesAsync(CourseParams CourseParams);
        void AddCourse(Course course);
        void Update(Course course);
        Task<Course> GetCourseByIdAsync(int id);
        void DisableCourse(Course course);
    }
}