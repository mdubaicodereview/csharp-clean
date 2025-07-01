using System;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.UI
{
    /// <summary>
    /// Helper class for console UI operations
    /// </summary>
    public static class ConsoleUI
    {
        /// <summary>
        /// Displays a header with a title surrounded by ASCII box
        /// </summary>
        /// <param name="title">The title to display</param>
        public static void DisplayHeader(string title)
        {
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine($"║{title.PadLeft((32 + title.Length) / 2).PadRight(32)}║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.WriteLine();
        }
        
        /// <summary>
        /// Displays a message and waits for a specified duration
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="delayMilliseconds">The delay in milliseconds</param>
        public static async Task DisplayMessageAsync(string message, int delayMilliseconds = 1000)
        {
            Console.WriteLine(message);
            await Task.Delay(delayMilliseconds);
        }
        
        /// <summary>
        /// Reads an integer from the console with validation
        /// </summary>
        /// <param name="prompt">The prompt to display</param>
        /// <returns>The validated integer value</returns>
        public static async Task<int> ReadIntegerAsync(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? string.Empty;
                
                if (int.TryParse(input, out int result))
                {
                    return await Task.FromResult(result);
                }
                
                await DisplayMessageAsync("Invalid input. Please enter a number.", 1500);
            }
        }
        
        /// <summary>
        /// Reads a non-empty string from the console with validation
        /// </summary>
        /// <param name="prompt">The prompt to display</param>
        /// <returns>The validated string value</returns>
        public static async Task<string> ReadStringAsync(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? string.Empty;
                
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return await Task.FromResult(input);
                }
                
                await DisplayMessageAsync("Input cannot be empty. Please try again.", 1500);
            }
        }
        
        /// <summary>
        /// Displays the main menu options
        /// </summary>
        public static void DisplayMenu()
        {
            Console.WriteLine("\n1. Add todo");
            Console.WriteLine("2. Mark as done");
            Console.WriteLine("3. Delete todo");
            Console.WriteLine("4. Exit");
            Console.Write("\nChoice: ");
        }
    }
}
