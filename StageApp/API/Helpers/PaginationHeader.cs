namespace API.Helpers
{
    internal class PaginationHeader
    {
        public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            this.CurrentPage = currentPage;
            this.ItemPerPage = itemsPerPage;
            this.TotalItems = totalItems;
            this.TotalPages = totalPages;
        }
        public int CurrentPage { get; }
        public int ItemPerPage { get; }
        public int TotalItems { get; }
        public int TotalPages { get; }
    }
}