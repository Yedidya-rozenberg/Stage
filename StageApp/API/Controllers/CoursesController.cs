using System.Threading.Tasks;
using API.Data.Repositories;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class CoursesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CoursesController(IUnitOfWork unitOfWork, IMapper mapper)

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PageList<CourseDto>>> GetCourses([FromQuery] CourseParams courseParams)
        {
            PageList<CourseDto> courses;
            if (!courseParams.MyCourses)
            {
                courses = await _unitOfWork.CourseRepository.GetCoursesAsync(courseParams);
            }
            else
            {
                var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUsername());
                courseParams.CurrentUser = user.UserName;
                courses = await _unitOfWork.CourseRepository.GetCoursesAsync(courseParams);
            }
            Response.AddPaginationHeader(
                   courses.CurrentPage,
                   courses.PageSize,
                   courses.TotalCount,
                   courses.TotalPages);
            return Ok(courses);
        }

        [HttpGet("{courseName}", Name = "GetCourse")]
        public async Task<ActionResult<CourseDto>> GetCourse(string courseName)
        {
            var course = await _unitOfWork.CourseRepository.GetCourseByNameAsync(courseName);
            if (course == null)
            {
                return NotFound();
            }
            if (course.CourseStatus == false)
            {
                var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUsername());
                if (user.Id != course.TeacherID)
                {
                    return BadRequest("Course is not active");
                }
            }
            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse(CourseUpdateDto courseUpdateDto)
        {
            var teacher = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUsername());
            if (teacher.GetType() != typeof(Teacher))
            {
                return Unauthorized("You are not a teacher");
            }
            var course = _mapper.Map<Course>(courseUpdateDto);
            course.Teacher = teacher as Teacher;
            _unitOfWork.CourseRepository.AddCourse(course);
            if (await _unitOfWork.Complete())
            {
                
                return CreatedAtRoute("GetCourse", new { courseName = course.CourseName }, _mapper.Map<CourseDto>(course));
            }
            return BadRequest("Could not add course");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CourseDto>> UpdateCourse(int id, CourseUpdateDto courseUpdateDto)
        {
            var teacher = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUsername());
            if (!(teacher is Teacher)) return Unauthorized("You are not a teacher");

            var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            if (teacher.Id != course.TeacherID) return Unauthorized("You are not the teacher of this course");

            _mapper.Map(courseUpdateDto, course);

            _unitOfWork.CourseRepository.Update(course);
            if (await _unitOfWork.Complete())
            {
                return Ok(_mapper.Map<CourseDto>(course));
            }
            return BadRequest("Could not update course");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseDto>> DeleteCourse(int id)
        {
            var teacher = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUsername());
            if (teacher.GetType() != typeof(Teacher)) return Unauthorized("You are not a teacher");

            var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            if (teacher.Id != course.TeacherID) return Unauthorized("You are not the teacher of this course");

            _unitOfWork.CourseRepository.DisableCourse(course);
            if (await _unitOfWork.Complete())
            {
                return Ok(_mapper.Map<CourseDto>(course));
            }
            return BadRequest("Could not delete course");
        }



    }
}