using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Services
{
    /// <summary>
    /// Service class for managing todo tasks
    /// </summary>
    public class TodoTaskService
    {
        private readonly List<TodoTask> _tasks;
        
        /// <summary>
        /// Initializes a new instance of the TodoTaskService class
        /// </summary>
        public TodoTaskService()
        {
            _tasks = new List<TodoTask>();
            InitializeSampleData();
        }
        
        /// <summary>
        /// Initializes sample data for demonstration purposes
        /// </summary>
        private void InitializeSampleData()
        {
            _tasks.Add(new TodoTask
            {
                Id = 1,
                Title = "Buy milk",
                IsCompleted = false,
                CreatedDate = DateTime.Now.AddDays(-1)
            });
            
            _tasks.Add(new TodoTask
            {
                Id = 2,
                Title = "Call mom",
                IsCompleted = false,
                CreatedDate = DateTime.Now.AddDays(-2)
            });
            
            _tasks.Add(new TodoTask
            {
                Id = 3,
                Title = "Finish report",
                IsCompleted = true,
                CreatedDate = DateTime.Now.AddDays(-3)
            });
        }
        
        /// <summary>
        /// Gets all tasks asynchronously
        /// </summary>
        /// <returns>A list of all tasks</returns>
        public async Task<IEnumerable<TodoTask>> GetAllTasksAsync()
        {
            // Simulate async operation
            return await Task.FromResult(_tasks);
        }
        
        /// <summary>
        /// Adds a new task asynchronously
        /// </summary>
        /// <param name="title">The title of the task</param>
        /// <returns>The newly created task</returns>
        public async Task<TodoTask> AddTaskAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Task title cannot be empty", nameof(title));
            }
            
            var newTask = new TodoTask
            {
                Id = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1,
                Title = title,
                IsCompleted = false,
                CreatedDate = DateTime.Now
            };
            
            _tasks.Add(newTask);
            
            // Simulate async operation
            return await Task.FromResult(newTask);
        }
        
        /// <summary>
        /// Marks a task as completed asynchronously
        /// </summary>
        /// <param name="id">The ID of the task to mark as completed</param>
        /// <returns>True if the task was found and marked as completed, otherwise false</returns>
        public async Task<bool> MarkTaskAsCompletedAsync(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            
            if (task == null)
            {
                return await Task.FromResult(false);
            }
            
            task.IsCompleted = true;
            return await Task.FromResult(true);
        }
        
        /// <summary>
        /// Deletes a task asynchronously
        /// </summary>
        /// <param name="id">The ID of the task to delete</param>
        /// <returns>True if the task was found and deleted, otherwise false</returns>
        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            
            if (task == null)
            {
                return await Task.FromResult(false);
            }
            
            _tasks.Remove(task);
            return await Task.FromResult(true);
        }
    }
}
