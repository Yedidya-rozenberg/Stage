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
            var course = await _context.Courses.Include(c => c.Units).Where(c => c.CourseID == unit.CourseID).FirstOrDefaultAsync();
            var NextUnit = await _context.Units.Where(u => u.UnitID == unit.NextUnitId).FirstOrDefaultAsync();
            var BackUnit = await _context.Units.Where(u => u.UnitID == unit.BackUnitId).FirstOrDefaultAsync();
            if (NextUnit?.Node.PreviousId != BackUnit.UnitID || BackUnit?.Node.NextId != NextUnit.UnitID)
            {
                return;
            }
            if (course.Units.Count > 0 && BackUnit == null && NextUnit == null)
            {
                return;
            }

            var newUnit = new Unit
            {
                CourseID = unit.CourseID,
                UnitName = unit.UnitName,
                StudyContent = unit.StudyContent,
                Questions = unit.Questions,
                Node = new LinkedEntityNode()
                {
                    NextId = NextUnit.UnitID,
                    PreviousId = BackUnit.UnitID
                },
            };
            if (NextUnit != null)
            {
                NextUnit.Node.PreviousId = newUnit.UnitID;
                _context.Units.Update(NextUnit);
            }
            if (BackUnit != null)
            {
                BackUnit.Node.NextId = newUnit.UnitID;
                _context.Units.Update(BackUnit);
            }
            course.Units.Add(_mapper.Map<Unit>(unit));
        }

        public async Task<UnitDto> GetUnitByIdAsync(int id)
        {

            var unit = await _context.Units.Include(u=>u.Node).FirstOrDefaultAsync(u => u.UnitID == id);
            return _mapper.Map<UnitDto>(unit);
        }

        public async Task<UnitDto> GetUnitByNameAsync(string name)
        {
            var unit = await _context.Units.Include(u=>u.Node).FirstOrDefaultAsync(u => u.UnitName == name);
            return _mapper.Map<UnitDto>(unit);
        }

        public async Task<PageList<UnitDto>> GetUnitsByCourseIdAsync(UnitParams unitParams)
        {
            var units = _context.Units.Include(u=>u.Node).AsQueryable();
            units = units.Where(u => u.CourseID == unitParams.CourseId).OrderBy(u => u.Node);
            return await PageList<UnitDto>.CreateAsync(units.ProjectTo<UnitDto>(_mapper.ConfigurationProvider).AsNoTracking(), unitParams.PageNumber, unitParams.PageSize);
        }

        public async void RemoveUnit(int unitID)
        {
            var unit = await _context.Units.FindAsync(unitID);
            var node = await _context.LinkedEntityNodes.FindAsync(unitID);
            var NextUnit = await _context.Units.Where(u => u.UnitID == node.NextId).Include(u=>u.Node).FirstOrDefaultAsync();
            var BackUnit = await _context.Units.Where(u => u.UnitID == node.PreviousId).Include(u=>u.Node).FirstOrDefaultAsync();
            if (NextUnit != null)
            {
                NextUnit.Node.PreviousId = BackUnit.UnitID;
                _context.Units.Update(NextUnit);
            }
            if (BackUnit != null)
            {
                BackUnit.Node.NextId = NextUnit.UnitID;
                _context.Units.Update(BackUnit);
            }
            _context.Units.Remove(unit);
        }


        public void Update(Unit unit)
        {
            _context.Entry<Unit>(unit).State = EntityState.Modified;
        }

    }
}
