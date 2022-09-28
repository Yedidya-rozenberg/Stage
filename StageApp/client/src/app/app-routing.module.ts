import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { NotFoundComponent } from './Errors/not-found/not-found.component';
import { CoursesComponent } from './courses/courses.component';
import { CourseComponent } from './course/course.component';
import { UnitComponent } from './unit/unit.component';
import { AccessableGuard } from './guards/accessable.guard';
import { UnitResolver } from './resolvers/unit.resolver';


const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },


  {
    path: '',
    canActivate: [AuthGuard],
    runGuardsAndResolvers: 'always',
    children: [
      { path: 'courses', component: CoursesComponent },
      {
        path: '',
        canActivate: [AccessableGuard],
        runGuardsAndResolvers: 'always',
        children: [
          { path: 'course', component: CourseComponent },
          {
            path: 'unit/:unitId', component: UnitComponent,
            resolve: { unit: UnitResolver }
          },
        ]
      }]
  },
  { path: 'not-found', component: NotFoundComponent },
  {
    path: '**',
    pathMatch: 'full',
    component: NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
