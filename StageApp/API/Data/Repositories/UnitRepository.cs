using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;

namespace API.Data.Repositories
{
    internal class UnitRepository : IUnitRepository
    {
        private readonly DataContext _context;

        public UnitRepository(DataContext context)
        {
            _context = context;
        }

        public void AddUnit(Unit unit, int CourseID, int TeacherID)
        {
            throw new System.NotImplementedException();
        }

        public Task<Unit> GetUnitByIdAsync(int id)
        {
            throw new System.NotImplementedException();
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