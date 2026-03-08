import { Component, OnInit } from '@angular/core';
import { TaskService } from '../../services/task.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Task } from '../../models/task';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {

  tasks: Task[] = [];
  members: any[] = [];

searchText = '';
statusFilter = '';
priorityFilter = '';
assigneeFilter: number | null = null;

 taskForm: any = {
  title: '',
  description: '',
  priority: 'Medium',
  status: 'Todo',
  assigneeId: null
};

  isEditing = false;

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.loadTasks();
    this.loadMembers();
  }
loadTasks() {
  this.taskService.getTasks(
    this.searchText,
    this.statusFilter,
    this.priorityFilter,
    this.assigneeFilter
  ).subscribe({
    next: (data) => {
      this.tasks = data;
    },
    error: (err) => {
      console.error(err);
    }
  });
}

loadMembers() {
  this.taskService.getMembers().subscribe({
    next: (data) => {
      this.members = data;
    },
    error: (err) => {
      console.error(err);
    }
  });
}

  search() {
    this.loadTasks();
  }

  applyFilters() {
    this.loadTasks();
  }

  openCreateForm() {

    this.isEditing = false;

    this.taskForm = {
      title: '',
      description: '',
      status: 'Todo',
      priority: 'Medium',
      assigneeId: null
    };

  }
createTask() {

  const payload = {
    title: this.taskForm.title,
    description: this.taskForm.description,
    priority: this.taskForm.priority,
    assigneeId: this.taskForm.assigneeId ? Number(this.taskForm.assigneeId) : null
  };

  this.taskService.createTask(payload).subscribe(() => {
    this.loadTasks();
    this.resetForm();
  });

}

  editTask(task: any) {

    this.isEditing = true;

    this.taskForm = { ...task };

  }

  updateTask() {

    this.taskService.updateTask(this.taskForm.id, this.taskForm)
      .subscribe(() => {

        this.taskForm = null;

        this.loadTasks();

      });

  }
resetForm() {
  this.taskForm = {
    title: '',
    description: '',
    status: 'Todo',
    priority: 'Medium',
    assigneeId: null
  };

  this.isEditing = false;
}
  deleteTask(id: number) {

    if (!confirm('Delete this task?')) return;

    this.taskService.deleteTask(id)
      .subscribe(() => {

        this.loadTasks();

      });

  }

  cancelForm() {

    this.taskForm = null;

  }

}