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
        string? input;
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

    public static int GetValidatedNumber(string prompt, string validationMessage, bool isPubYear = false)
    {
        string? input;
        bool isValidNumber;
        int resultNumber;
        bool invalidCondition;
        do
        {
            Console.WriteLine(prompt);
            input = Console.ReadLine()?.Trim();
            isValidNumber = int.TryParse(input, out resultNumber);
            invalidCondition = isPubYear ? !isValidNumber || resultNumber < 0 || resultNumber > DateTime.Now.Year : !isValidNumber;

            if (invalidCondition)
            {
                Console.WriteLine($"{validationMessage}");
            }
        } while (invalidCondition);

        return resultNumber;
    }
}