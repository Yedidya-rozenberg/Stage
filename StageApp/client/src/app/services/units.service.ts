import { Injectable } from '@angular/core';
import { unit } from '../models/unit';
import { unitName } from '../models/unitName';
import { unitParams } from '../models/params/unitParams';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/pagination';
import { HttpClient } from '@angular/common/http';
import { Observable, of, tap } from 'rxjs';
import { getPaginatedResult, getPaginationParams } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class UnitsService {

  baseUrl = environment.apiUrl;
  unitsCache = new Map<string, PaginatedResult<unitName[]>>();
  unitParams: unitParams = new unitParams;


  constructor(private http: HttpClient) { }

  getUnits(unitParams: unitParams): Observable<PaginatedResult<unitName[]>> {
    const cacheKay = Object.values(unitParams).join('-');
    const response = this.unitsCache.get(cacheKay);
    if (response) { return of(response); }

    let params = getPaginationParams(unitParams.pageNumber, unitParams.pageSize);
    params = params.append('courseId', unitParams.courseId.toString());

    return getPaginatedResult<unitName[]>(this.baseUrl + 'course/Units', params, this.http)
      .pipe(tap(response => { this.unitsCache.set(cacheKay, response); }));
  }
}
