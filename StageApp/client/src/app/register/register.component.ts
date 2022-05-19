import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model:any = {};
  @Input() usersFromHomeComponent: any;
  @Output() cancelRegister = new EventEmitter<boolean>();
  registerForm!: FormGroup;
  maxDate: Date | undefined;
  validationErrors: string[] = [];

  constructor(
    private accountService: AccountService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private router:Router) { }

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);

  }
  register(){
    this.accountService.register(this.registerForm.value).subscribe(
      (data) => {
      this.router.navigate(['/members'])      
      this.cancel();
      },
      error =>{
        console.log(error);
        this.validationErrors = error;
      } 
    )
    // console.log(this.registerForm.value);
  }
  cancel(){
    this.cancelRegister.emit(false);
  }

  initializeForm(){

    this.registerForm = this.fb.group({
      gender:['male'],
      knownAs:["", Validators.required],
      city:["", Validators.required],
      country:["", Validators.required],
      dateOfBirth:["", Validators.required],
      username:["", Validators.required],
      password:["", [Validators.required, Validators.minLength(4),Validators.maxLength(8)]],
      confirmPassword:["",[ Validators.required, this.matchValue("password")]] 
    });
    this.registerForm.get("password")?.valueChanges.subscribe(()=>{
      this.registerForm.get("confirmPassword")?.updateValueAndValidity;
    })
  }
matchValue(matchTo:string):ValidatorFn{
  return(control:AbstractControl):ValidationErrors |null => {
      const ControlValue = control.value;
      const ControlToMatch = (control?.parent as FormGroup)?.controls[matchTo];
      const ControlToMatchValue = ControlToMatch?.value;
      return (ControlValue === ControlToMatchValue) ? null : {IsMatching: true}
    
  }
}

}