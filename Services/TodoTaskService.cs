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
                CreatedDate = DateTime.Now.AddDays(-1),
                Category = "Shopping",
                Tags = new List<string> { "urgent" }
            });
            
            _tasks.Add(new TodoTask
            {
                Id = 2,
                Title = "Call mom",
                IsCompleted = false,
                CreatedDate = DateTime.Now.AddDays(-2),
                Category = "Personal",
                Tags = new List<string> { "important" }
            });
            
            _tasks.Add(new TodoTask
            {
                Id = 3,
                Title = "Finish report",
                IsCompleted = true,
                CreatedDate = DateTime.Now.AddDays(-3),
                Category = "Work",
                Tags = new List<string> { "urgent", "important" }
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
        /// Gets tasks filtered by category asynchronously
        /// </summary>
        /// <param name="category">The category to filter by</param>
        /// <returns>A list of tasks in the specified category</returns>
        public async Task<IEnumerable<TodoTask>> GetTasksByCategoryAsync(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return await GetAllTasksAsync();
            }
            
            var filteredTasks = _tasks.Where(t => t.Category == category);
            return await Task.FromResult(filteredTasks);
        }
        
        /// <summary>
        /// Gets all unique categories from existing tasks
        /// </summary>
        /// <returns>A list of unique categories</returns>
        public async Task<IEnumerable<string>> GetUniqueCategoriesAsync()
        {
            var categories = _tasks
                .Where(t => !string.IsNullOrWhiteSpace(t.Category))
                .Select(t => t.Category)
                .Distinct();
                
            return await Task.FromResult(categories);
        }
        
        /// <summary>
        /// Adds a new task asynchronously
        /// </summary>
        /// <param name="title">The title of the task</param>
        /// <param name="category">The category of the task (optional)</param>
        /// <param name="tags">The tags for the task (optional)</param>
        /// <returns>The newly created task</returns>
        public async Task<TodoTask> AddTaskAsync(string title, string? category = default, List<string>? tags = default)
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
                CreatedDate = DateTime.Now,
                Category = category ?? string.Empty,
                Tags = tags ?? new List<string>()
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
