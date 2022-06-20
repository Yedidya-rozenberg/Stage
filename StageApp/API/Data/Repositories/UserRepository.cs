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
    public class UserRepository : IUserRepository
    {
        public readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users
            .SingleOrDefaultAsync<AppUser>(x => x.UserName == userName);
        }
        public void Update(AppUser user)
        {
            _context.Entry<AppUser>(user).State = EntityState.Modified;
        }


        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PageList<MemberDto>> GetMembersAsync(UserParams userParams)
        {

            if (userParams.CourseId == null)
            {
                var query = _context.Users.AsQueryable();
                return await PageList<MemberDto>.CreateAsync(
                    query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking(),
                    userParams.PageNumber, userParams.PageSize);
            }
            else
            {
                var query = _context.Students.Include(s=>s.Courses).AsQueryable();

                query = query.Where(s => s.Courses.Any(c => c.CourseID == userParams.CourseId));
                return await PageList<MemberDto>.CreateAsync(
                    query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking(),
                    userParams.PageNumber, userParams.PageSize);
            }
        }
    }

}