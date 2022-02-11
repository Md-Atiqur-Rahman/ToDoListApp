import { Injectable } from '@angular/core';
import { TodoItem } from '../models/todo-item.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Login } from '../models/login.model';
import { User } from '../models/register.model';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class TodoAppService {
  readonly baseURL = "https://localhost:44336/api/ToDoItem";
  readonly authURL = "https://localhost:44336/api/Authentication";
  list: any[]=[];
  dashboardlist: any[]=[];
  yesNoSelected:any;
  reminderdate;
  duedate;
  constructor(private http: HttpClient) { }
  todoData: TodoItem = new TodoItem();
  userName:any;
  loginData: User = new User();
  registerData: User = new User();
  postToDoItem() {
    console.log(this.todoData,'postToDoItem');
    this.todoData.userName = localStorage.getItem("loginUser");
    return this.http.post(this.baseURL+'/Add', this.todoData, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }
  putToDoItem() {
    console.log(this.todoData.todoItemId);
    
// return this.http.put(this.baseURL+'/update/todoItemId/'+this.todoData.todoItemId,this.todoData, {
//     headers: new HttpHeaders({
//       "Content-Type": "application/json"
//     })
//   });
    // return this.http.put(`${this.baseURL+'/update/todoItemId?'}
    // +${this.todoData.todoItemId}`, this.todoData, {
    //   headers: new HttpHeaders({
    //     "Content-Type": "application/json"
    //   })
    // });
    return this.http.post(this.baseURL+'/update', this.todoData, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  deleteToDoItem(id: number) {
    return this.http.get<any>(this.baseURL+'/delete/'+id);
  }

  refreshList() {
    this.http.get(this.baseURL+'/get-all')
    .toPromise()
    .then(res => {
      this.list = (<any>res).apiData as TodoItem[];
     
    });
  }
 todaysTodoItem() {
    this.http.get(this.baseURL+'/get-todaysTodoItemList')
    .toPromise()
    .then(res => {
      this.dashboardlist = (<any>res).apiData as TodoItem[];
    });
  }

  loginUser() {
    this.loginData.dateOfBirth= "2022-02-10T08:56:58.805Z";
    console.log(this.loginData,'loginUser');
    //Himel89
    return this.http.post(`${this.authURL}/login`, this.loginData, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  registerUser(): Observable<any> {
    
    console.log(this.registerData,'this.registerData');
    return this.http.post(`${this.authURL}/register`, this.registerData, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  getTypeList(data?: any): Observable<any> {
    return this.http.get<any>(this.baseURL + '/getToDoItemTypes', data);
  }
}
