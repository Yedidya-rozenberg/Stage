export interface Pagination {
     currentPage: number,
     itemsPerPage: number,
     totalItems: number,
     totalPage: number
}

export class PaginatedResult<T>{
     result: T | undefined;
     pagination: Pagination | undefined;
}
