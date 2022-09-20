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
export class UnitResolver implements Resolve<unit> {
  constructor(private unitService: UnitsService) { }
  resolve(route: ActivatedRouteSnapshot): Observable<unit> {
    return this.unitService.getUnit(route.paramMap.get('unitId') as string);
  }
}
