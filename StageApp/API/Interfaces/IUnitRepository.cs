using System;
using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace API.Interfaces
{
    public interface IUnitRepository
    {
        void AddUnit(Unit unit, int CourseID,  int TeacherID);

        void RemoveUnit(int unitID, Course Course, int TeacherID);

        void Update(Unit unit,  int TeacherID);
        Task<Unit> GetUnitByIdAsync(int id);    
    }
}