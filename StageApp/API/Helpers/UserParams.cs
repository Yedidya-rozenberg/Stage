using System;

namespace API.Helpers
{
    public class UserParams : PaginationParams
    {
        public int? CourseId { get; set; }
    }
}