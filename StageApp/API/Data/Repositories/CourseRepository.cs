using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CourseRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async void AddCourse(Course course)
        {
            await _context.Courses.AddAsync(course);
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }


        public void Update(Course course)
        {
            _context.Entry<Course>(course).State = EntityState.Modified;
        }


        public async Task<PageList<CourseDto>> GetCoursesAsync(CourseParams CourseParams)
        {

            var user = await _context.Users.SingleOrDefaultAsync(c => c.UserName == CourseParams.CurrentUser);

            var query = _context.Courses.AsQueryable();
            if (user is Student && CourseParams.MyCourses)
            {
                query = query.Where(c => c.Students.Any(s => s.Id == user.Id));
            }
            else if (user is Teacher && CourseParams.MyCourses)
            {
                query = query.Where(c => c.TeacherID == user.Id);
            }
            else
            {
                query = query.Where(c => c.CourseStatus);
            }


            if (CourseParams.TeacherName != null)
            {
                var queryByTeacher = _context.Courses
                 .Include(c => c.Teacher)
                 .Where(c => c.Teacher.KnownAs.ToLower() == CourseParams.TeacherName.ToLower()).Select(c => c).AsQueryable();

                query = from c in query
                        join ct in queryByTeacher on c.CourseID equals ct.CourseID
                        select c;
            }

            query = query.OrderBy(c => c.CourseID);


            return await PageList<CourseDto>.CreateAsync(
                query.ProjectTo<CourseDto>(_mapper.ConfigurationProvider).AsNoTracking(),
                CourseParams.PageNumber, CourseParams.PageSize);
        }

        public async void DisableCourse(Course course)
        {
            var courseToDisable = await _context.Courses.Include(c => c.Students).Where(c => c == course).FirstOrDefaultAsync();
            foreach (var student in courseToDisable.Students)
            {
                courseToDisable.Students.Remove(student);
            }
            courseToDisable.CourseStatus = false;
        }

        public async Task<CourseDto> GetCourseByNameAsync(String name)
        {
            var course = await _context.Courses.Include(c => c.Photo).Include(c => c.Teacher).Where(c => c.CourseName == name).FirstOrDefaultAsync();
            return _mapper.Map<CourseDto>(course);
        }

        public async Task<IEnumerable<MemberDto>> GetStudentsByCourseIdAsync(int id)
        {
            var course = await _context.Courses.Include(c => c.Students).Where(c => c.CourseID == id).FirstOrDefaultAsync();
            return _mapper.Map<IEnumerable<MemberDto>>(course.Students);
        }

        public async void RegisterStudentToCourse(int courseId, Student student)
        {
            var course = await _context.Courses.Include(c => c.Students).Where(c => c.CourseID == courseId).FirstOrDefaultAsync();
            course.Students.Add(student);
        }
    
        public void UnregisterStudentFromCourse(int courseId, Student student)
        {
            var course = _context.Courses.Include(c => c.Students).Where(c => c.CourseID == courseId).FirstOrDefault();
            course.Students.Remove(student);
        }
      

        public Task<bool> CheckStudentCourse(int courseId, int studentId)
        {
            var course = _context.Courses.Include(c => c.Students).Where(c => c.CourseID == courseId).FirstOrDefault();
            return Task.FromResult(course.Students.Any(s => s.Id == studentId));
        }
     
    }
}