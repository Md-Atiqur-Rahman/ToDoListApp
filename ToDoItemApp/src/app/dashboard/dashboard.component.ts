import { Component, OnInit } from '@angular/core';
import { TodoAppService } from '../services/todo-app.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

 
  constructor(public service: TodoAppService) { }

  ngOnInit() {
    this.service.todaysTodoItem();
  }

}
