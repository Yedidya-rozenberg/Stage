export interface Pagination{
     currentPage: number,
     itemPerPage: number,
     totalItems: number,
     totalPage: number
}

export class PaginatedResult<T>{
result: T | undefined;
pagination:Pagination | undefined;
}
