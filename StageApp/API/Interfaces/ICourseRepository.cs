using System.Reflection.Metadata;
using System;
using API.Helpers;
using System.Threading.Tasks;
using API.Entities;
using System.Collections.Generic;

namespace API.Interfaces

{
    public interface ICourseRepository
    {
       Task<PageList<Course>> ViewCuorsesList(CourseParams CourseParams);
       void addCourse(Course course, int TeacherID);
       void RemoveCourse(Course course, int TeacherID);
       void Update(int courseID, Course course, int TeacherID);
       Task<bool> SaveAllAsync();
    }
}