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
        Task<PageList<CourseDto>> ViewCuorsesList(CourseParams CourseParams);
        void addCourse(Course course);
        void UpdateCourseStatus(Course course, bool CourseStatus);
        void Update(Course course);

        Task<Course> GetCourseByIdAsync(int id);
        Task<bool> SaveAllAsync();


    }
}