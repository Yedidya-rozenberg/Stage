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
        void AddUnit(CreateUnitDto unit);

        void RemoveUnit(int unitID);

        void Update(Unit unit);
        Task<UnitDto> GetUnitByIdAsync(int id);

        Task<UnitDto> GetUnitByNameAsync(string name);


        Task<PageList<UnitDto>> GetUnitsByCourseIdAsync(UnitParams unitParams);

    }
}