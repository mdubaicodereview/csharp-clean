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
                string title = await ConsoleUI.ReadStringAsync("Enter todo title: ");
                var newTask = await _taskService.AddTaskAsync(title);
                
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
    }
}
