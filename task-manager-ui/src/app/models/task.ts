export interface Task {
  id: number
  title: string
  description: string
  status: string
  priority: string
  assigneeId?: number
  createdAt: Date
  assignee?: {
    id: number;
    name: string;
    email: string;
  };
}