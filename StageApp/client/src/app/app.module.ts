import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { MembersComponent } from './members/members.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './modules/shared.module';
import { RegisterComponent } from './register/register.component';
import { TextInputComponent } from './forms/text-input/text-input.component';
import { DateInputComponent } from './forms/date-input/date-input.component';
import { CoursesComponent } from './courses/courses.component';
import { CourseCardComponent } from './course-card/course-card.component';
import { CourseComponent } from './course/course.component';
import { UnitComponent } from './unit/unit.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { LoadingInterceptor } from './interceptors/loading.interceptor';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { DetilesEditComponent } from './detiles-edit/detiles-edit.component';
import { FooterComponent } from './footer/footer.component';
import { NotFoundComponent } from './Errors/not-found/not-found.component';
import { ServerErrorComponent } from './Errors/server-error/server-error.component';
import { TestErrorComponent } from './Errors/test-error/test-error.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    MembersComponent,
    RegisterComponent,
    TextInputComponent,
    DateInputComponent,
    CoursesComponent,
    CourseCardComponent,
    CourseComponent,
    UnitComponent,
    DetilesEditComponent,
    FooterComponent,
    NotFoundComponent,
    ServerErrorComponent,
    TestErrorComponent
  ],
  imports: [
    SharedModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoadingInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
