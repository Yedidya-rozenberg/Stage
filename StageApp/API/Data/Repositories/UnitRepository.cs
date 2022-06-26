using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    internal class UnitRepository : IUnitRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UnitRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddUnit(Unit unit, int CourseID, int TeacherID)
        {
            throw new System.NotImplementedException();
        }

        public Task<UnitDto> GetUnitByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<UnitDto>> GetUnitsByCourseIdAsync(int courseID)
        {
            var course = await _context.Courses.Include(c => c.Units).FirstOrDefaultAsync(c => c.CourseID == courseID);
            var units = _mapper.Map<IEnumerable<Unit>, IEnumerable<UnitDto>>(course.Units);
            return units;
        }

        public void RemoveUnit(int unitID, Course Course, int TeacherID)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Unit unit, int TeacherID)
        {
            throw new System.NotImplementedException();
        }
    }
}
