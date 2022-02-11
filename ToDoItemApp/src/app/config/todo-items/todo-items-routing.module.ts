import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TodoItemFormComponent } from './todo-item/todo-item-form/todo-item-form.component';
import { TodoItemComponent } from './todo-item/todo-item.component';


const routes: Routes = [
  { path: '', component: TodoItemComponent },
  { path: 'todoItem', component: TodoItemComponent },
  { path: 'todoForm', component: TodoItemFormComponent },
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TodoItemsRoutingModule { }
