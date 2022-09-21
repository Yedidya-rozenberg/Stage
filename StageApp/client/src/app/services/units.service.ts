import { Injectable } from '@angular/core';
import { unit } from '../models/unit';
import { unitName } from '../models/unitName';
import { unitParams } from '../models/params/unitParams';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/pagination';
import { HttpClient } from '@angular/common/http';
import { map, Observable, Observer, of, pipe, tap } from 'rxjs';
import { getPaginatedResult, getPaginationParams } from './paginationHelper';
import { CoursesService } from './courses.service';

@Injectable({
  providedIn: 'root'
})
export class UnitsService {



  baseUrl = environment.apiUrl;
  unitsCache = new Map<string, PaginatedResult<unitName[]>>();
  fullUnitCache:  unit[] = [];
  unitParams: unitParams = new unitParams;
  editMode: boolean = false;


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

  getUnit(unitId: string): Observable<unit> {
    let unit = this.fullUnitCache.find(x => x.unitID.toString() === unitId);
    if (unit !== undefined) {
      return of(unit);
    }
    return this.http.get<unit>(this.baseUrl + 'course/Units/' + unitId).pipe(
      tap((unit: unit) => {
        this.fullUnitCache.push(unit);
      }
      )
    );
  }

  updateUnit(unit: unit): Observable<unit> {
    return this.http.post<unit>(this.baseUrl + 'course/Units/' + unit.unitID.toString(), unit)
    .pipe(map((unit: unit) => {
      this.unitsCache.clear();
      this.fullUnitCache[this.fullUnitCache.findIndex(x => x.unitID == unit.unitID)] = unit;
      return (unit);
    } ));
  }

  addUnit(courseID: number) {
    throw new Error('Method not implemented.');
  }

   removeUnit(unitId: number) {
   return  this.http.delete<PaginatedResult<unitName[]>>(this.baseUrl + 'course/Units/' + unitId.toString())
     .pipe(tap(() => {
        this.unitsCache.clear();
        delete this.fullUnitCache[this.fullUnitCache.findIndex(x => x.unitID === unitId)];
      }));
  }
}


