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
    public class DeleteAnOrderWorkflow
    {
        internal static void Execute()
        {
            Console.Clear();

            OrderManager manager = OrderManagerFactory.Create();

            var userDateTimeInPut = ConsoleIO.GetFutureDateTime("Please enter a date. EX: MM/DD/YYYY ");
            OrderDateLookupResponse response = manager.OrderLookupDate(userDateTimeInPut);
            if (response.Success)
            {
                foreach (var order in response.ListOfOrders)
                {
                    Console.WriteLine($"Order Number: {order.OrderNumber}, Customer Name: {order.CustomerName}, State {order.State}, Tax Rate: {order.TaxRate}, Area: {order.Area}, Cost Per Square Foot: {order.CostPerSquareFoot}, Labor Cost Per Square Foot: {order.LaborCostPerSquareFoot}, Material Cost: {order.MaterialCost}, Labor Cost: {order.LaborCost}, Tax Total: {order.TotalTax}, Total Cost: {order.TotalCost}");
                }

                int number = ConsoleIO.GetOrderNumber("Which Order number would you like to remove? Enter the Order number you want to remove. ");

                if (ConsoleIO.GetYesNoAnswerFromUser($"Are you sure you want to remove this file?") == "Y")
                {
                    DeleteOrderResponse deleteResponse = manager.DeleteOrder(userDateTimeInPut, number);
                    if (deleteResponse.Success)
                    {
                        Console.WriteLine("The Order was successfully deleted.");
                        Console.WriteLine("Press any key to continue...");
                    }
                    else
                    {
                        Console.WriteLine("An error occurred.");
                        Console.WriteLine(deleteResponse.Message);
                    }
                }
                else
                {
                    Console.WriteLine("A delete order was cancelled. Press any key to continue.");
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("An error has occurred");
                Console.WriteLine(response.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
