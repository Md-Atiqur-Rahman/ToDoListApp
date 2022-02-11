import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TodoItem } from 'src/app/models/todo-item.model';
import { TodoAppService } from 'src/app/services/todo-app.service';

@Component({
  selector: 'app-todo-item',
  templateUrl: './todo-item.component.html',
  styleUrls: ['./todo-item.component.scss']
})
export class TodoItemComponent implements OnInit {

  constructor(public service: TodoAppService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.service.refreshList();
  }
  populateForm(selectedRecord: TodoItem) {
    //alert(JSON.stringify(selectedRecord.reminderDateTime))
    this.service.todoData = Object.assign({}, selectedRecord);
    this.service.yesNoSelected=this.service.todoData.isCompleted == false ? 'N':'Y';
    this.service.reminderdate = new Date(selectedRecord.reminderDateTime)
  }

  onDelete(id: number) {
    if (confirm("Are you sure you want to delete this record?")) {
      this.service.deleteToDoItem(id)
      .subscribe( res => {
        this.service.refreshList();
        this.toastr.error("Deleted susseccfully", "ToDo Item Register");
      },
      err => {
        console.log(err);
      })
    }
  }
}
