using System;
using System.Threading.Tasks;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IRequestRepository
    {
         void AddRequest(Request request);
        Task<PageList<Request>> GetNewRequests(DateTime time);
        void UpdateRequestStatus(int requestID, string newStatus);
        void MakeRequest(Request request);
        Task<bool> SaveAllAsync();
    }
}