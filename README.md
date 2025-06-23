# TaskManagementWebApi
Create a simple web api to create and read Users and Tasks.
Users are defined by a unique Name .
Tasks must have a unique Title and a State (Waiting, InProgress, Completed).

When a Task is created, a User should be assigned to it automatically. If no users are
available, the task should stay in Waiting state.

Users can have multiple tasks, Task can only have one user.

Every 2 minutes all tasks should be reassigned to another random user:
• It cannot be the user which is currently assigned to the task
• It cannot be the user which was assigned to the task on the previous round
• It can be a user which was assigned to the task two or more iterations before

When no users are available the Task will stay in Waiting state without an assigned user.
