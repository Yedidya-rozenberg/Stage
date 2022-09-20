using System.Threading.Tasks;
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

    [Route("api/course/[controller]")]
    public class UnitsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UnitsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PageList<UnitNameDto>>> GetUnits([FromQuery] UnitParams unitParams)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUsername());
            var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(unitParams.CourseId);
            if (course == null)
            {
                return NotFound();
            }
            if (course.CourseStatus == false && user.UserName != course.Teacher.UserName)
            {
                return BadRequest("Course is not active");
            }
            if (course.TeacherID == user.Id || await _unitOfWork.CourseRepository.CheckStudentCourse(unitParams.CourseId, user.Id))
            {
                var units = await _unitOfWork.UnitRepository.GetUnitsByCourseIdAsync(unitParams);
                Response.AddPaginationHeader(
                        units.CurrentPage,
                        units.PageSize,
                        units.TotalCount,
                        units.TotalPages);
                return Ok(units);
            }
            else
            {
                return Unauthorized("You are not authorized to access this course");
            }
        }

        [HttpGet("{unitID}", Name = "GetUnit")]
        public async Task<ActionResult<UnitDto>> GetUnit(int unitID)
        {
            var unit = await _unitOfWork.UnitRepository.GetUnitByIdAsync(unitID);
            if (unit == null)
            {
                return NotFound();
            }
            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUsername());
            var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(unit.CourseID);
            if (course == null)
            {
                return NotFound();
            }
            if (course.CourseStatus == false)
            {
                return BadRequest("Course is not active");
            }
            if (course.TeacherID == user.Id || await _unitOfWork.CourseRepository.CheckStudentCourse(unit.CourseID, user.Id))
            {
                return Ok(_mapper.Map<UnitDto>(unit));
            }
            return Unauthorized("You are not authorized to access this course");
        }

        [HttpPut]
        public async Task<ActionResult<UnitDto>> CreateUnit(CreateUnitDto unit)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUsername());
            var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(unit.CourseID);
            if (course == null)
            {
                return NotFound();
            }
            if (course.TeacherID != user.Id)
            {
                return Unauthorized("You are not authorized to updaete this course");
            }
            _unitOfWork.UnitRepository.AddUnit(unit);
            if (await _unitOfWork.Complete())
            {
                var newUnit = await _unitOfWork.UnitRepository.GetUnitByNameAsync(unit.UnitName);
                return CreatedAtRoute("GetUnit", new { unitID = newUnit.UnitID }, unit);
            }
            return BadRequest("Could not add unit");
        }

        [HttpDelete("{unitID}")]
        public async Task<ActionResult<PageList<UnitNameDto>>> DeleteUnit(int unitID)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUsername());
            var unit = await _unitOfWork.UnitRepository.GetUnitByIdAsync(unitID);
            if (unit == null)
            {
                return NotFound();
            }
            var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(unit.CourseID);
            if (course == null)
            {
                return NotFound();
            }
            if (course.TeacherID != user.Id)
            {
                return Unauthorized("You are not authorized to updaete this course");
            }
            _unitOfWork.UnitRepository.RemoveUnit(unitID);
            if (await _unitOfWork.Complete())
            {
                return Ok(await _unitOfWork.UnitRepository.GetUnitsByCourseIdAsync(new UnitParams { CourseId = unit.CourseID }));
            }
            return BadRequest("Could not delete unit");
        }

        [HttpPost("{unitID}")]
        public async Task<ActionResult<UnitDto>> UpdateUnit(UnitDto unit)
        {
            var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(unit.CourseID);
            if (course == null)
            {
                return NotFound();
            }
            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUsername());
            if (course.TeacherID != user.Id)
            {
                return Unauthorized("You are not authorized to updaete this course");
            }
            var unitFromRepo = await _unitOfWork.UnitRepository.GetUnitByIdAsync(unit.UnitID);
            if (unitFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(unit,unitFromRepo);
            this._unitOfWork.UnitRepository.Update(unitFromRepo);
            if (await _unitOfWork.Complete())
            {
                return Ok(await _unitOfWork.UnitRepository.GetUnitByNameAsync(unit.UnitName));
            }
            return BadRequest("Could not update unit");
        }
    }
}