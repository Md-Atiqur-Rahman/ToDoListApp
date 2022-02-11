import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { TodoItemsRoutingModule } from './todo-items-routing.module';
import { TodoItemComponent } from './todo-item/todo-item.component';
import { TodoItemFormComponent } from './todo-item/todo-item-form/todo-item-form.component';
import { MatCardModule, MatDatepickerModule, MatFormFieldModule, MatIconModule, MatInputModule, MatListModule, MatNativeDateModule, MatSelectModule, MatSidenavModule, MatTableModule, MatToolbarModule, MatTooltipModule, MatTreeModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OverlayModule } from '@angular/cdk/overlay';
import { CdkTableModule } from '@angular/cdk/table';
import { CdkTreeModule } from '@angular/cdk/tree';
import { CdkStepperModule } from '@angular/cdk/stepper';
import { TimePickersComponent } from './time-pickers/time-pickers.component';


@NgModule({
  declarations: [TodoItemComponent, TodoItemFormComponent, TimePickersComponent],
  imports: [
    CommonModule,
    TodoItemsRoutingModule,
    FormsModule,
    FlexLayoutModule,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    MatListModule,
    MatInputModule,
    MatCardModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatFormFieldModule, 
    
    // ReactiveFormsModule,
    // MatButtonModule,
    // MatMenuModule,
    // MatDatepickerModule,
    // MatNativeDateModule,
    // MatTableModule,
    // MatToolbarModule,
    MatToolbarModule,
    MatTooltipModule,
    CdkStepperModule,
    OverlayModule,
    CdkTableModule,
    CdkTreeModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTableModule,
    MatTreeModule,
  ]
})
export class TodoItemsModule { }
