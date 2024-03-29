using System;

namespace API.Helpers
{
    public class PaginationParams
    {
        private const int _maxPageSize = 50;
        public int PageNumber { get; set; }
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Min(_maxPageSize, value);
        }
    }
}