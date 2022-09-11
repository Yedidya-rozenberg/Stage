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
    internal class UnitRepository : IUnitRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UnitRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async void AddUnit(CreateUnitDto unit)
        {
            var course = await _context.Courses.Include(c=>c.Units).Where(c=>c.CourseID== unit.CourseID).FirstOrDefaultAsync();
            course.Units.Add(_mapper.Map<Unit>(unit));
        }

        public async Task<UnitDto> GetUnitByIdAsync(int id)
        {

            var unit = await _context.Units.FindAsync(id);
            return _mapper.Map<UnitDto>(unit);
        }

        public async Task<UnitDto> GetUnitByNameAsync(string name)
        {
            var unit = await _context.Units.FirstOrDefaultAsync(u => u.UnitName == name);
            return _mapper.Map<UnitDto>(unit);
        }

        public async Task<PageList<UnitDto>> GetUnitsByCourseIdAsync(UnitParams unitParams)
        {
            var units = _context.Units.AsQueryable();
            units = units.Where(u=>u.CourseID ==unitParams.CourseId).OrderBy(u=>u.UnitID);
            return await PageList<UnitDto>.CreateAsync(units.ProjectTo<UnitDto>(_mapper.ConfigurationProvider).AsNoTracking(), unitParams.PageNumber, unitParams.PageSize);
        }

        public void RemoveUnit(int unitID)
        {
            var unit = _context.Units.Find(unitID);
            _context.Units.Remove(unit);
        }


        public void Update(Unit unit)
        {
            _context.Entry<Unit>(unit).State = EntityState.Modified;
        }

    }
}
