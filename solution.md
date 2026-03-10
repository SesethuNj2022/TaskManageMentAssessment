Task Management System – Solution Design
Overview
This document describes the architecture, design decisions, and trade-offs made when
implementing the Task Management system. The goal of the project was to build a backend API
that allows teams to manage tasks and track work.
Core Features
• View tasks
• Search tasks
• Filter tasks by status, priority, and assignee
• Create new tasks
• Update existing tasks
• Delete tasks
• Assign tasks to team members
• Update task priorities
Architecture
The system follows a layered architecture separating presentation, application logic, and data
access.
Angular UI → API Controllers → Service Layer → EF Core → InMemory Database
Controller Layer
Controllers handle HTTP requests, validate input, and delegate work to services. Examples include
TasksController and TeamMembersController.
Service Layer
The service layer contains business logic and prevents controllers from directly interacting with the
database. This improves maintainability and testability.
Data Layer
Entity Framework Core with the InMemory provider is used for persistence. This allows the
application to run without requiring an external database.
Domain Models
TaskItem: Represents a task with properties such as title, description, status, priority, and assignee.
TeamMember: Represents a user that tasks can be assigned to.
Enums
TaskStatus: Todo, InProgress, Done
TaskPriority: Low, Medium, High
Error Handling
The API returns meaningful HTTP status codes including 200 (OK), 201 (Created), 204 (No
Content), 400 (Bad Request), 404 (Not Found), and 500 (Server Error). Global exception
middleware ensures consistent error responses.
Testing Strategy
Unit tests are implemented using xUnit. Tests run against an EF Core InMemory database and
each test creates a unique database instance to ensure isolation.
Design Trade-offs
Using an InMemory database simplifies setup but does not persist data between runs. A service
layer adds structure but improves maintainability and testing.
Potential Improvements
• Add authentication (JWT)
• Add pagination for large task lists
• Implement structured logging
• Add integration tests
• Replace InMemory database with SQL Server or PostgreSQL