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
            IQueryable<Course> query;


            query = CourseParams.Role switch
            {
                "Student" => _context.Students.Include(s => s.Courses).Where(s => s.UserName == CourseParams.CurrentUser).Select(s => s.Courses).FirstOrDefault().AsQueryable(),
                "Teacher" => _context.Teachers.Include(t => t.Courses).Where(t => t.UserName == CourseParams.CurrentUser).Select(t => t.Courses).FirstOrDefault().AsQueryable(),
                _ => _context.Courses.Where(c => c.CourseStatus == true).AsQueryable(),
            };

            if (CourseParams.TeacherName != null)
            {
                var queryByTeacher = _context.Courses
                 .Include(c => c.Teacher)
                 .Where(c => c.Teacher.KnownAs.ToLower() == CourseParams.TeacherName.ToLower());

                query = from c in query
                        join ct in queryByTeacher on c.CourseID equals ct.CourseID
                        select c;
            }


            return await PageList<CourseDto>.CreateAsync(
                query.ProjectTo<CourseDto>(_mapper.ConfigurationProvider).AsNoTracking(),
                CourseParams.PageNumber, CourseParams.PageSize);
        }

        public async void DisableCourse(Course course)
        {
            var courseToDisable = await _context.Courses.Include(c=>c.Students).Where(c=>c==course).FirstOrDefaultAsync();
            foreach (var student in courseToDisable.Students)
            {
               courseToDisable.Students.Remove(student);
            }
            courseToDisable.CourseStatus = false;
        }
    }
}