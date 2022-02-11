export class TodoItem {
    todoItemId: number=0;
    todoItemType:  number=0;
    todoItemContent: string="";
    todoItemDate: Date=new Date();
    userName: string="";
    reminderDateTime: string | undefined;
    dueDateTime: string| undefined;
    isCompleted: boolean| undefined;
}
