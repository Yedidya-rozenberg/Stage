using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using AutoMapper;

namespace API.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IUserRepository UserRepository => new UserRepository(_context, _mapper);
        public ICourseRepository CourseRepository => new CourseRepository(_context, _mapper);
        public IUnitRepository UnitRepository => new UnitRepository(_context, _mapper);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
  
}