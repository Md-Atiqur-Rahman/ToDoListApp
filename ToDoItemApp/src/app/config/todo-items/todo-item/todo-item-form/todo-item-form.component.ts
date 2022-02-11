import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { TodoItem } from 'src/app/models/todo-item.model';
import { TodoAppService } from 'src/app/services/todo-app.service';

@Component({
  selector: 'app-todo-item-form',
  templateUrl: './todo-item-form.component.html',
  styleUrls: ['./todo-item-form.component.scss']
})
export class TodoItemFormComponent implements OnInit {
  constructor(public service: TodoAppService,
    private toastr: ToastrService,
    public datepipe: DatePipe
    ) { }
    yesNoDropDown:any;
    typeList:any;
    selected:any;
  ngOnInit() {
    this.getTypeList();
    this.yesNoDropDown=[
      {key : 'Y', value : "Yes"},
      {key : 'N', value : "No"}
    ]
  }
  isOpenRem = false;
  isOpen = false;
 
  public onSetDueDate(newDate: Date) {
    this.isOpen = false;
    this.service.duedate = newDate;
   var date= this.datepipe.transform(this.service.duedate, 'yyyy-MM-dd hh:mm');
    this.service.todoData.dueDateTime=date
    //alert(this.service.todoData.dueDateTime+" onSetDueDate");
  }
  public onSetRemDate(newDate: Date) {
    this.isOpenRem = false;
    this.service.reminderdate = newDate;
    var rdate= this.datepipe.transform(this.service.reminderdate, 'yyyy-MM-dd hh:mm');
    this.service.todoData.reminderDateTime=rdate;
    //alert(this.service.todoData.reminderDateTime);
  }
  getTypeList()
  {
    this.service.getTypeList().subscribe(
      data => {
        console.log(data);
        this.typeList=data.apiData;
      },
      error => {
        console.log(error);
      }
    );
  }
  onChangeType(e:any){
    this.selected = e.target.value;
    if(this.selected !=2)
    {
      this.service.todoData.reminderDateTime=null;
    }
    this.service.todoData.isCompleted=false;
  }


  ChangeIsComplete(e:any){
    if(e =='Y')
    {
     this.service.todoData.isCompleted =true;
      this.service.todoData.dueDateTime=null;
      this.service.duedate ==null;
    }
    else{
      this.service.todoData.isCompleted=false;
    }
  }


  onSubmit(form: NgForm) {
    if(this.service.todoData.dueDateTime==null){
      this.service.todoData.dueDateTime=this.service.duedate;
    }
    if(this.service.todoData.reminderDateTime==null){
      this.service.todoData.reminderDateTime=this.service.reminderdate;
    }
    console.log(this.service.todoData);
    if (this.service.todoData.todoItemId == 0) {
      console.log("Hello");
      this.insertRecord(form);
    } else {
      this.updateRecord(form);
    }

  }

  insertRecord(form: NgForm) {
    this.service.postToDoItem().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success("Added successfully", "ToDo Item");
      },
      err => { console.log(err); }
    );
  }
  resetForm(form: NgForm) {
    form.form.reset();
    this.service.todoData = new TodoItem();
  }
  updateRecord(form: NgForm) {
    this.service.putToDoItem().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.info("Updated successfully", "ToDo Item Register");
      },
      err => { console.log(err); }
    );
  }
}
