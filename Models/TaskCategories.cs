using System;
using System.Collections.Generic;

namespace TodoApp.Models
{
    /// <summary>
    /// Provides predefined categories and tags for todo tasks
    /// </summary>
    public static class TaskCategories
    {
        /// <summary>
        /// Predefined categories for tasks
        /// </summary>
        public static readonly List<string> Categories = new List<string>
        {
            "Work",
            "Personal",
            "Shopping",
            "Health",
            "Finance"
        };

        /// <summary>
        /// Predefined tags for tasks
        /// </summary>
        public static readonly List<string> Tags = new List<string>
        {
            "urgent",
            "important",
            "can-wait",
            "delegated",
            "in-progress"
        };
    }
}
