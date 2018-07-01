using SWCCorp.BLL;
using SWCCorp.Models;
using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.UI.Workflows
{
    public class AddAnOrderWorkflow
    {
        internal static void Execute()
        {
            Console.Clear();
            Console.WriteLine("Add an Order ");
            Console.WriteLine("*************************************");
            Console.WriteLine();

            OrderManager manager = OrderManagerFactory.Create();

            DateTime orderDate = ConsoleIO.GetFutureDateTime("Enter a valid Date EX: MM/DD/YYYY ");
            string customerName = ConsoleIO.GetStringFromUser("Customer Name: ");

            TaxLookupResponse taxResponse = manager.LoadTaxes();
            var state = ConsoleIO.GetStateFromUser("State: ", taxResponse.Taxes);

            IEnumerable<Product> GetProducts = manager.GetAllProducts();
            ConsoleIO.ShowListOfProducts(GetProducts);

            ProductLookupResponse productResponse = manager.LoadProducts();
            var productType = ConsoleIO.GetProductFromUser("Product Type: ", productResponse.Products);
            decimal area = ConsoleIO.GetAreaFromUser("Area: ");

            Console.WriteLine();
            Console.WriteLine("*************************************");

          
            AddAnOrderResponse addResponse = manager.AddOrder(orderDate, customerName, state, productType, area, 0);  
            
            if (!addResponse.Success)
            {
                Console.WriteLine("An error has occured");
                Console.WriteLine(addResponse.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Customer Name: {customerName}, State: {state.Abbreviation}, Product Type: {productType.Name}, Area: {area}");
                Console.WriteLine();
                if (ConsoleIO.GetYesNoAnswerFromUser($"Are you sure you want to add this file?") == "Y")
                {
                    manager.AddToOrderRepo(addResponse.AddedOrder);
                    Console.WriteLine("The Order was successfully added.");
                    Console.WriteLine("Press any key to continue...");
                }
                else
                {
                    Console.WriteLine("Order cancelled. Press any key to continue.");
                    Console.WriteLine("Press any key to continue...");
                }
                Console.ReadLine();
            }
        }
    }
}
