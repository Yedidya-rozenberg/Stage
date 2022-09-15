import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MembersComponent } from './members/members.component';
import { AuthGuard } from './guards/auth.guard';
import { TestErrorComponent } from './Errors/test-error/test-error.component';
import { NotFoundComponent } from './Errors/not-found/not-found.component';
import { ServerErrorComponent } from './Errors/server-error/server-error.component';
import { CoursesComponent } from './courses/courses.component';
import { CourseComponent } from './course/course.component';
import { UnitComponent } from './unit/unit.component';
import { DetilesEditComponent } from './detiles-edit/detiles-edit.component';
import { AccessableGuard } from './guards/accessable.guard';


const routes: Routes = [
  {  path: '', component: HomeComponent,pathMatch: 'full'},     


  {
    path: '',
    canActivate: [AuthGuard],
    runGuardsAndResolvers: 'always',
    children: [
      { path: 'courses', component: CoursesComponent },
      {path: '',
      canActivate: [AccessableGuard],
      runGuardsAndResolvers: 'always',
      children: [
      { path: 'users', component: MembersComponent },
      { path: 'course/:courseName', component: CourseComponent },
      { path: 'unit/:unitName', component: UnitComponent },
      { path: 'member/edit', component: DetilesEditComponent },
    ]}]
  },
  {
    path: 'errors', component: TestErrorComponent
  },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
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
