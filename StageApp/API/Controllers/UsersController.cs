using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Extantion;
using API.Extensions;
using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController: BaseApiController
    {   
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            this._context = context;
        }  
       [HttpGet]
        public async Task<ActionResult<PagedList<AppUser>>> GetUsers([FromQuery]UserParams userParams)
        {
            var query =  _context.Users.AsNoTracking();
            var pagedList = await PagedList<AppUser>.CreateAsync(
                query, userParams.PageNumber, userParams.PageSize);
            Response.AddPaginationHeader(
                pagedList.CurrentPage,pagedList.PageSize, pagedList.TotalCount, pagedList.TotalPages);

            return Ok(pagedList);
        }
    
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }
    }
}