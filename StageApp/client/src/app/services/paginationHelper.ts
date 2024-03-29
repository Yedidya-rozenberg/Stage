import { HttpClient, HttpParams, HttpResponse } from "@angular/common/http";
import { map, Observable } from "rxjs";
import { PaginatedResult } from "../models/pagination";

export function getPaginatedResult<T>(url: string, params: HttpParams, http: HttpClient): Observable<PaginatedResult<T>> {
  const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
  return http.get<T>(url,
    {
      observe: 'response',
      params
    }).pipe(
      map((res: HttpResponse<T>) => {
        paginatedResult.result = res.body as T;
        if (res.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(res.headers.get('Pagination') || '');
        }
        return paginatedResult;
      })
    );
}


export function getPaginationParams(pageNumber: number, pageSize: number) {
  let params = new HttpParams();
  params = params.append('pageNumber', pageNumber.toString());
  params = params.append('pageSize', pageSize.toString());
  return params;
}