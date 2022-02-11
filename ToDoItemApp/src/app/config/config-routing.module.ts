import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardService } from '../services/auth-guard.service';


const routes: Routes = [
  {
    path: 'todoItems',
    loadChildren: () =>
      import('./todo-items/todo-items.module').then((m) => m.TodoItemsModule),
      canActivate: [AuthGuardService],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConfigRoutingModule { }
