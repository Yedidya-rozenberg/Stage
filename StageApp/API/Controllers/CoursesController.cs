using System.Collections.Generic;
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

        [AllowAnonymous]
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
                if (user == null)
                {
                    return Unauthorized();
                }
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

            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUsername());
            if (course.CourseStatus == false)
            {
                if (user.Id != course.TeacherID)
                {
                    return BadRequest("Course is not active");
                }
                return Ok(course);
            }
            if (await _unitOfWork.CourseRepository.CheckStudentCourse(course.CourseID, user.Id))
            {
                return Ok(course);
            }
            return Unauthorized("You are not authorized to access this course");
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

        //register student to course
        [HttpPut("register")]
        public async Task<ActionResult<CourseDto>> RegisterStudent(int courseId)
        {
            var student = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUsername());
            if (student.GetType() != typeof(Student)) return Unauthorized("You are not a student");
            var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(courseId);
            if (course == null) return NotFound();
            if (course.CourseStatus == false) return BadRequest("Course is not active");
            if (await _unitOfWork.CourseRepository.CheckStudentCourse(courseId, student.Id)) return BadRequest("You are already registered to this course");
            _unitOfWork.CourseRepository.RegisterStudentToCourse(courseId, student as Student);
            if (await _unitOfWork.Complete())
            {
                return Ok(_mapper.Map<CourseDto>(course));
            }
            return BadRequest("Could not register student");
        }

        //unregister student from course
        [HttpDelete("unregister")]
        public async Task<ActionResult> UnregisterStudent(int courseId)
        {
            var student = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUsername());
            if (student.GetType() != typeof(Student)) return Unauthorized("You are not a student");
            var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(courseId);
            if (course == null) return NotFound();
            if (course.CourseStatus == false) return BadRequest("Course is not active");
            if (!await _unitOfWork.CourseRepository.CheckStudentCourse(courseId, student.Id)) return BadRequest("You are not registered to this course");
            _unitOfWork.CourseRepository.UnregisterStudentFromCourse(courseId, student as Student);
            if (await _unitOfWork.Complete())
            {
                return Ok("Student unregistered");
            }
            return BadRequest("Could not unregister student");
        }

        [HttpGet("students/{courseId}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetStudents(int courseId)
        {
            var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(courseId);
            if (course == null) return NotFound();
            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUsername());
            if (user.Id != course.TeacherID) return Unauthorized("You are not the teacher of this course");
            var students = await _unitOfWork.CourseRepository.GetStudentsByCourseIdAsync(courseId);
            return Ok(students);
        }
    }
}