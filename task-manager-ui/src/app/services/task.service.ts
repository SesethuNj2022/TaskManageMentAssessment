import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from '../models/task';

@Injectable({
providedIn: 'root'
})
export class TaskService {

private apiUrl = 'http://localhost:5181/api/tasks';

constructor(private http: HttpClient) {}


// getTasks() {
//   return this.http.get<Task[]>(this.apiUrl);
// }
getTasks(search?: string, status?: string, priority?: string, assigneeId?: number | null) {

  let url = 'http://localhost:5181/api/tasks?';

  if (search) url += `search=${search}&`;
  if (status) url += `status=${status}&`;
  if (priority) url += `priority=${priority}&`;
  if (assigneeId) url += `assigneeId=${assigneeId}&`;

  return this.http.get<any[]>(url);
}createTask(task: any) {
  return this.http.post('http://localhost:5181/api/tasks', task);
}

updateTask(id: number, task: any) {
  return this.http.put(`${this.apiUrl}/${id}`, task);
}

deleteTask(id: number) {
  return this.http.delete(`${this.apiUrl}/${id}`);
}

getMembers() {
  return this.http.get<any[]>('http://localhost:5181/api/team-members');
}

}