using System;
using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;

namespace API.Interfaces
{
    public interface IUnitRepository
    {
        void AddUnit(Unit unit, int CourseID,  int TeacherID);

        void RemoveUnit(int unitID, Course Course, int TeacherID);

        void Update(Unit unit,  int TeacherID);
        Task<UnitDto> GetUnitByIdAsync(int id);   

        Task<IEnumerable<UnitDto>> GetUnitsByCourseIdAsync(int courseID);

    }
}