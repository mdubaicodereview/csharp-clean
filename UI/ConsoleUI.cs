using System;
using System.Collections.Generic;
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
            Console.WriteLine("4. Filter by category");
            Console.WriteLine("5. Exit");
            Console.Write("\nChoice: ");
        }
        
        /// <summary>
        /// Displays a list of options and prompts the user to select one
        /// </summary>
        /// <param name="prompt">The prompt to display</param>
        /// <param name="options">The list of options to display</param>
        /// <param name="includeNoneOption">Whether to include a 'None' option</param>
        /// <returns>The selected option or null if 'None' is selected</returns>
        public static async Task<string?> SelectFromListAsync(string prompt, IEnumerable<string> options, bool includeNoneOption = true)
        {
            var optionsList = new List<string>(options);
            
            Console.WriteLine($"\n{prompt}");
            
            for (int i = 0; i < optionsList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {optionsList[i]}");
            }
            
            if (includeNoneOption)
            {
                Console.WriteLine($"{optionsList.Count + 1}. None");
            }
            
            int selection = await ReadIntegerAsync($"\nSelect option (1-{(includeNoneOption ? optionsList.Count + 1 : optionsList.Count)}): ");
            
            if (selection > 0 && selection <= optionsList.Count)
            {
                return optionsList[selection - 1];
            }
            else if (includeNoneOption && selection == optionsList.Count + 1)
            {
                return null;
            }
            
            await DisplayMessageAsync("Invalid selection. Please try again.", 1500);
            return await SelectFromListAsync(prompt, options, includeNoneOption);
        }
        
        /// <summary>
        /// Allows the user to select multiple options from a list
        /// </summary>
        /// <param name="prompt">The prompt to display</param>
        /// <param name="options">The list of options to display</param>
        /// <returns>A list of selected options</returns>
        public static async Task<List<string>> SelectMultipleFromListAsync(string prompt, IEnumerable<string> options)
        {
            var optionsList = new List<string>(options);
            var selectedOptions = new List<string>();
            
            Console.WriteLine($"\n{prompt}");
            
            for (int i = 0; i < optionsList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {optionsList[i]}");
            }
            
            Console.WriteLine("\nEnter numbers separated by commas (e.g., 1,3,4) or 0 to select none:");
            string input = await ReadStringAsync("Selection: ");
            
            if (input == "0")
            {
                return selectedOptions;
            }
            
            try
            {
                foreach (string indexStr in input.Split(','))
                {
                    if (int.TryParse(indexStr.Trim(), out int index) && index > 0 && index <= optionsList.Count)
                    {
                        selectedOptions.Add(optionsList[index - 1]);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayMessageAsync($"Error parsing selection: {ex.Message}", 1500);
            }
            
            return selectedOptions;
        }
    }
}
