using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Utility;

public class Helper
{
    public static string GetValidatedInput(string prompt)
    {
        string input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be empty. Please try again.");
            }
        } while (string.IsNullOrWhiteSpace(input));

        return input;
    }

    public static int GetValidatedNumber(string prompt, string validationMessage)
    {
        string input;
        bool isValidNumber;
        int resultNumber;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine()?.Trim();
            isValidNumber = int.TryParse(input, out resultNumber);
            if (!isValidNumber)
            {
                Console.WriteLine($"{validationMessage}");
            }
        } while (!isValidNumber);

        return resultNumber;
    }

}
