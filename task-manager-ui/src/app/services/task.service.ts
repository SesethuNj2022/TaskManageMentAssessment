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


getTasks() {
  return this.http.get<Task[]>(this.apiUrl);
}

createTask(task: any) {
  return this.http.post(this.apiUrl, task);
}

updateTask(id: number, task: any) {
  return this.http.put(`${this.apiUrl}/${id}`, task);
}

deleteTask(id: number) {
  return this.http.delete(`${this.apiUrl}/${id}`);
}

getMembers(): Observable<any[]> {

return this.http.get<any[]>(`${this.apiUrl}/team-members`);

}

}