import { Component, EventEmitter, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TodoAppService } from '../services/todo-app.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  invalidLogin: boolean | undefined;
  isLoggedIn = false;
  constructor(public service: TodoAppService,
    private toastr: ToastrService, private router: Router) { }

  ngOnInit() {
  }
  login(form: NgForm) {
    this.service.loginUser().subscribe(
      res => {
        //console.log(res.apiData,'res data');
        const token = (<any>res).apiData.token;
        const userName = (<any>res).apiData.username;
        localStorage.setItem("jwt", token);
        localStorage.setItem("loginUser", userName);
        this.service.userName=userName;
        this.invalidLogin = false;
        this.isLoggedIn = true;
        //this.router.navigate(["/"]);
        this.router.navigate(['/dashboard']);
        //this.router.navigate(['/config/todoItems']);
        //this.router.navigate(['/dashboard']);
        this.toastr.success("Wellcome " + userName);
      },
      err => {
        this.invalidLogin = true;
       // console.log(err);
        this.toastr.success("Invalid credentials");
      }
    );
  }
}
