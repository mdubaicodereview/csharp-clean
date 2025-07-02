using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Models;
using TodoApp.Services;
using TodoApp.UI;

namespace TodoApp
{
    /// <summary>
    /// Main program class for the Todo application
    /// </summary>
    public class Program
    {
        private static TodoTaskService _taskService = new TodoTaskService();
        
        /// <summary>
        /// Entry point of the application
        /// </summary>
        public static async Task Main(string[] args)
        {
            bool running = true;
            
            while (running)
            {
                await DisplayTasksAsync();
                ConsoleUI.DisplayMenu();
                
                try
                {
                    int choice = int.Parse(Console.ReadLine() ?? "0");
                    
                    switch (choice)
                    {
                        case 1:
                            await AddTodoAsync();
                            break;
                        case 2:
                            await MarkTodoAsDoneAsync();
                            break;
                        case 3:
                            await DeleteTodoAsync();
                            break;
                        case 4:
                            await FilterByCategoryAsync();
                            break;
                        case 5:
                            running = false;
                            break;
                        default:
                            await ConsoleUI.DisplayMessageAsync("Invalid choice! Please try again.", 1500);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    await ConsoleUI.DisplayMessageAsync($"An error occurred: {ex.Message}", 2000);
                }
            }
        }
        
        /// <summary>
        /// Displays all tasks
        /// </summary>
        private static async Task DisplayTasksAsync()
        {
            Console.Clear();
            ConsoleUI.DisplayHeader("TODO MANAGER");
            
            var tasks = await _taskService.GetAllTasksAsync();
            
            foreach (var task in tasks)
            {
                Console.WriteLine(task.ToString());
            }
        }
        
        /// <summary>
        /// Adds a new todo task
        /// </summary>
        private static async Task AddTodoAsync()
        {
            Console.Clear();
            ConsoleUI.DisplayHeader("ADD NEW TODO");
            
            try
            {
                // Get task title
                string title = await ConsoleUI.ReadStringAsync("Enter todo title: ");
                
                // Get task category
                string? category = await ConsoleUI.SelectFromListAsync(
                    "Select a category:", 
                    TaskCategories.Categories);
                
                // Get task tags
                List<string> tags = await ConsoleUI.SelectMultipleFromListAsync(
                    "Select tags (multiple allowed):", 
                    TaskCategories.Tags);
                
                // Create the new task
                var newTask = await _taskService.AddTaskAsync(title, category, tags);
                
                await ConsoleUI.DisplayMessageAsync("Todo added successfully!");
            }
            catch (ArgumentException ex)
            {
                await ConsoleUI.DisplayMessageAsync($"Error: {ex.Message}", 2000);
            }
        }
        
        /// <summary>
        /// Marks a todo task as done
        /// </summary>
        private static async Task MarkTodoAsDoneAsync()
        {
            Console.Clear();
            ConsoleUI.DisplayHeader("MARK AS COMPLETE");
            
            try
            {
                int id = await ConsoleUI.ReadIntegerAsync("Enter todo ID: ");
                bool result = await _taskService.MarkTaskAsCompletedAsync(id);
                
                if (result)
                {
                    await ConsoleUI.DisplayMessageAsync("Todo marked as done!");
                }
                else
                {
                    await ConsoleUI.DisplayMessageAsync("Todo not found!");
                }
            }
            catch (Exception ex)
            {
                await ConsoleUI.DisplayMessageAsync($"Error: {ex.Message}", 2000);
            }
        }
        
        /// <summary>
        /// Deletes a todo task
        /// </summary>
        private static async Task DeleteTodoAsync()
        {
            Console.Clear();
            ConsoleUI.DisplayHeader("DELETE TODO");
            
            try
            {
                int id = await ConsoleUI.ReadIntegerAsync("Enter todo ID to delete: ");
                bool result = await _taskService.DeleteTaskAsync(id);
                
                if (result)
                {
                    await ConsoleUI.DisplayMessageAsync("Todo deleted successfully!");
                }
                else
                {
                    await ConsoleUI.DisplayMessageAsync("Todo not found!");
                }
            }
            catch (Exception ex)
            {
                await ConsoleUI.DisplayMessageAsync($"Error: {ex.Message}", 2000);
            }
        }
        
        /// <summary>
        /// Filters tasks by category
        /// </summary>
        private static async Task FilterByCategoryAsync()
        {
            Console.Clear();
            ConsoleUI.DisplayHeader("FILTER BY CATEGORY");
            
            try
            {
                // Get all unique categories from tasks
                var categories = await _taskService.GetUniqueCategoriesAsync();
                var categoriesList = new List<string>(categories);
                
                if (categoriesList.Count == 0)
                {
                    await ConsoleUI.DisplayMessageAsync("No categories found!", 2000);
                    return;
                }
                
                // Add option to show all tasks
                categoriesList.Add("All Tasks");
                
                // Let user select a category
                string? selectedCategory = await ConsoleUI.SelectFromListAsync("Select a category to filter by:", categoriesList, false);
                
                Console.Clear();
                
                if (selectedCategory == null)
                {
                    // Handle null case, though it shouldn't happen with includeNoneOption=false
                    await DisplayTasksAsync();
                }
                else if (selectedCategory == "All Tasks")
                {
                    await DisplayTasksAsync();
                }
                else
                {
                    ConsoleUI.DisplayHeader($"TASKS IN {selectedCategory.ToUpper()}");
                    var filteredTasks = await _taskService.GetTasksByCategoryAsync(selectedCategory);
                    
                    foreach (var task in filteredTasks)
                    {
                        Console.WriteLine(task.ToString());
                    }
                }
                
                Console.WriteLine("\nPress any key to return to main menu...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                await ConsoleUI.DisplayMessageAsync($"Error: {ex.Message}", 2000);
            }
        }
    }
}
