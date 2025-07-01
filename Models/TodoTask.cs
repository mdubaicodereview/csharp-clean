using System;

namespace TodoApp.Models
{
    /// <summary>
    /// Represents a todo task in the application
    /// </summary>
    public class TodoTask
    {
        /// <summary>
        /// Unique identifier for the task
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Title of the task
        /// </summary>
        public string Title { get; set; } = string.Empty;
        
        /// <summary>
        /// Indicates whether the task is completed
        /// </summary>
        public bool IsCompleted { get; set; }
        
        /// <summary>
        /// Date when the task was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// Creates a string representation of the task
        /// </summary>
        public override string ToString()
        {
            string status = IsCompleted ? "[X]" : "[ ]";
            return $"{Id}. {status} {Title} ({CreatedDate:yyyy-MM-dd})";
        }
    }
}
