using SWCCorp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.UI
{
    public static class ConsoleIO
    {
        internal static void ShowListOfProducts(IEnumerable<Product> products)
        {
            Console.WriteLine();
            Console.WriteLine("*************************************");
            foreach (var product in products)
            {
                Console.Write("Product Name: ");
                Console.WriteLine(product.Name);
                Console.Write("Material Cost: ");
                Console.WriteLine(product.MaterialUnitCost);
                Console.Write("Labor Cost: ");
                Console.WriteLine(product.LaborUnitCost);
                Console.WriteLine();
            }
        }
        public static void CopySeedFiles(string fullPath, string tempPath)
        {
            File.Delete(fullPath);
            File.Copy(tempPath, fullPath);
        }
        internal static int GetOrderNumber(string prompt)
        {
            bool valid = false;
            int output = -1;
            while (!valid)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out output))
                {
                    Console.WriteLine("You must type in a valid integer");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (output < 1)
                    {
                        Console.WriteLine("Please choose number greater than 1. ");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        valid = true;
                    }
                }
            }
            return output;
        }

        internal static DateTime GetDateTimeFromUser()
        {
            DateTime toReturn = DateTime.MaxValue;
            bool valid = false;

            while (!valid)
            {
                Console.Write("Enter an Order Date EX: MM/DD/YYYY : ");
                string userInput = Console.ReadLine();

                valid = DateTime.TryParse(userInput, out toReturn);

            }

            return toReturn;
        }

        internal static DateTime GetFutureDateTime(string prompt)
        {
            DateTime dateTime = DateTime.MaxValue;
            bool valid = false;
            while (!valid)
            {
                Console.Write(prompt);
                string userInput = Console.ReadLine();

                if (!DateTime.TryParse(userInput, out dateTime))
                {
                    Console.WriteLine("You must type in a valid future date");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }

                else
                {
                    if (dateTime <= DateTime.Today)
                    {
                        Console.WriteLine("Date must be in the future");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        valid = true;
                    }
                }
            }
            return dateTime;
        }

        internal static string GetYesNoAnswerFromUser(string prompt)
        {
            bool valid = false;
            string input = null;

            while (!valid)
            {
                Console.Write(prompt + " (Y/N)? ");
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must enter Y/N.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (input != "Y" && input != "N")
                    {
                        Console.WriteLine("You must enter Y/N.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        valid = true;
                    }
                }
            }
            return input;
        }

        internal static Tax GetStateFromUser(string prompt, IEnumerable<Tax>taxes)
        {
            Tax userInput = null;
            bool valid = false; 

            while (!valid)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if ((input == String.Empty) || (input.Length < 2 || input.Length > 2))
                {
                    Console.WriteLine("You must type in a valid state abbreviation.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    userInput = taxes.SingleOrDefault(t => t.Abbreviation.ToUpper() == input.ToUpper());
                    valid = userInput != null;
                }
            }
            return userInput;
        }

        internal static string EditGetStateFromUser(Order order, string prompt, IEnumerable<Tax> taxes)
        {
            Tax taxUserInput = null;
            bool valid = false;

            while (!valid)
            {
                Console.Write(prompt);
                string userInput = Console.ReadLine();

                if (string.IsNullOrEmpty(userInput))
                {
                    userInput = order.State;
                    return userInput;
                }
                else if ((userInput == " ") || (userInput.Length < 2 || userInput.Length > 2))
                {
                    Console.WriteLine("You must type in a valid state abbreviation.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    taxUserInput = taxes.SingleOrDefault(t => t.Abbreviation.ToUpper() == userInput.ToUpper());
                    valid = userInput != null;
                }
            }
            return taxUserInput.Abbreviation;
        }

        internal static Product GetProductFromUser(string prompt, IEnumerable<Product> products)
        {
            Product productUserInput = null;
            bool valid = false;

            while (!valid)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().ToLower();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must type in a valid product.");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    productUserInput = products.SingleOrDefault(p => p.Name == input);
                    valid = productUserInput != null;
                }
            }
            return productUserInput;
        }

        internal static string EditGetProductFromUser(Order order, string prompt, IEnumerable<Product> products)
        {
            Product productUserInput = null;
            bool valid = false;

            while (!valid)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    input = order.ProductType;
                    return input;
                }
                else if (input == " ")
                {
                    Console.WriteLine("You must enter in valid text");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    productUserInput = products.SingleOrDefault(p => p.Name == input);
                    valid = productUserInput != null;
                }
            }
            return productUserInput.Name;
        }
        internal static string GetStringFromUser(string prompt)
        {
            bool valid = false;
            string input = null;
            while (!valid)
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must type in valid text");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (input == " ")
                {
                    Console.WriteLine("You must enter in valid text");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    valid = true;
                }
            }
            return input;
        }

        internal static string EditGetStringFromUser(Order order, string prompt)
        {
            bool valid = false;
            string input = null;

            while (!valid)
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    input = order.CustomerName;
                    return input;
                }
                else if (input == " ")
                {
                    Console.WriteLine("You must enter in valid text");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    valid = true;
                }
            }
            return input;
        }

        internal static int GetAreaFromUser(string prompt)
        {
            bool valid = false;
            int output = -1;

            while (!valid)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out output))
                {
                    Console.WriteLine("You must type in a valid integer");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (output < 100)
                    {
                        Console.WriteLine("Please choose number greater than 100. ");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        valid = true;
                    }
                }
            }
            return output;
        }
        internal static decimal EditGetAreaFromUser(Order order, string prompt)
        {
            bool valid = false;
            decimal output = -1;

            while (!valid)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (String.IsNullOrEmpty(input))
                {
                    return order.Area;
                }
                else if (!decimal.TryParse(input, out output))
                {
                    Console.WriteLine("You must type in a valid integer");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (output < 100)
                    {
                        Console.WriteLine("Please choose number greater than 100. ");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        valid = true;
                    }
                }
            }
            return output;
        }
    }
}
