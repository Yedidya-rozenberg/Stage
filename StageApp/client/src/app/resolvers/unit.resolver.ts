import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { unit } from '../models/unit';
import { UnitsService } from '../services/units.service';

@Injectable({
  providedIn: 'root'
})
export class UnitResolver implements Resolve<Partial<unit>> {
  constructor(private unitService: UnitsService) { }
  resolve(route: ActivatedRouteSnapshot): Observable<Partial<unit>> {
    let title = route.paramMap.get('unitId');
    if (title === "new") {
      return of({});
    }
    return this.unitService.getUnit(title as string);
  }
}
