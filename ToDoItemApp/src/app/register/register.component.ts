import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from '../models/register.model';
import { TodoAppService } from '../services/todo-app.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(public service: TodoAppService,
    private toastr: ToastrService, private router: Router) { }

  ngOnInit() {
  }

  // error?: string | null;

  // submitEM = new EventEmitter();
  // form: FormGroup = new FormGroup({
  //   username: new FormControl(''),
  //   password: new FormControl(''),
  // });

  // submit() {
  //   if (this.form.valid) {
  //     //this.submitEM.emit(this.form.value);
  //     this.router.navigate(['/dashboard']);
  //     console.log(this.router.navigate(['/dashboard']));
  //   }
  // }

  // LogIn() {
  //   this.router.navigate(['/dashboard']);
  //   console.log(this.router.navigate(['/dashboard']));
  // }

//start
 isOpen = false;
  date;

  public onSetDate(newDate: Date) {
    
    this.isOpen = false;
    this.date = newDate;
  }
isSuccessful = false;
register(form: NgForm) {
  this.service.registerData.dateOfBirth=this.date;
  this.service.registerUser().subscribe(
    res => {
      this.resetForm(form);
      //this.service.refreshList();
      this.toastr.success("Submitted successfully", "User Register");
      this.isSuccessful=true;
    },
    err => { console.log(err); }
  );
}
resetForm(form: NgForm) {
  form.form.reset();
  this.service.registerData = new User();
}

}
