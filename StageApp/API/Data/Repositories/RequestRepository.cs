using System;
using System.Threading.Tasks;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;

namespace API.Data.Repositories
{
    internal class RequestRepository : IRequestRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RequestRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
  
        public void AddRequest(Request request)
        {
            throw new NotImplementedException();
        }

        public Task<PageList<Request>> GetNewRequests(DateTime time)
        {
            throw new NotImplementedException();
        }

        public void MakeRequest(Request request)
        {
            throw new NotImplementedException();
        }

        public void UpdateRequestStatus(int requestID, string newStatus)
        {
            throw new NotImplementedException();
        }
    }
}