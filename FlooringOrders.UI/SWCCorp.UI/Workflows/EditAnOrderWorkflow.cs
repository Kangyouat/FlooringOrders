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
    public class EditAnOrderWorkflow
    {
        public static void Execute()
        {
            Console.Clear();
            Console.WriteLine("Edit Order");
            Console.WriteLine("*************************************");

            OrderManager manager = OrderManagerFactory.Create();

            var userDateTimeInPut = ConsoleIO.GetDateTimeFromUser();
            OrderDateLookupResponse orderDateResponse = manager.OrderLookupDate(userDateTimeInPut);
            if (orderDateResponse.Success)
            {
                foreach (var order in orderDateResponse.ListOfOrders)
                {
                    Console.WriteLine($"Order Number: {order.OrderNumber}, Customer Name: {order.CustomerName}, State {order.State}, Tax Rate: {order.TaxRate}, Area: {order.Area}, Cost Per Square Foot: {order.CostPerSquareFoot}, Labor Cost Per Square Foot: {order.LaborCostPerSquareFoot}, Material Cost: {order.MaterialCost}, Labor Cost: {order.LaborCost}, Tax Total: {order.TotalTax}, Total Cost: {order.TotalCost}");
                }
                int number = ConsoleIO.GetOrderNumber("Enter the Order number do you want to edit?");
                
                var originalOrder = new Order(orderDateResponse.ListOfOrders.SingleOrDefault(f => f.OrderNumber == number));
                Order updatedOrder = new Order(originalOrder);

                TaxLookupResponse taxesResponse = manager.LoadTaxes();
                ProductLookupResponse productResponse = manager.LoadProducts();

                Console.Clear();
                updatedOrder.OrderDate = userDateTimeInPut;
                updatedOrder.CustomerName = ConsoleIO.EditGetStringFromUser(updatedOrder, $"(Previous Name: {originalOrder.CustomerName}) Enter Customer Name: ");

                updatedOrder.State = ConsoleIO.EditGetStateFromUser(updatedOrder, $"(Previous State: {originalOrder.State}) State: ", taxesResponse.Taxes);
                updatedOrder.ProductType = ConsoleIO.EditGetProductFromUser(updatedOrder, $"(Previous Type: {originalOrder.ProductType}) Product Type: ", productResponse.Products);
                updatedOrder.Area = ConsoleIO.EditGetAreaFromUser(updatedOrder, $"(Previous Area: {originalOrder.Area}) Area: ");

                Console.WriteLine($"Customer Name: {updatedOrder.CustomerName}, State: {updatedOrder.State}, Product Type: {updatedOrder.ProductType}, Area: {updatedOrder.Area}");
                Console.WriteLine();
                if (ConsoleIO.GetYesNoAnswerFromUser($"Are you sure you want to add this file?") == "Y")
                {
                    EditOrderResponse editResponse = manager.EditOrder(updatedOrder);
                    if (editResponse.Success)
                    {
                        Console.WriteLine("The Order was successfully updated.");
                        Console.WriteLine("Press any key to continue...");
                    }
                    else
                    {
                        Console.WriteLine("An error occurred.");
                        Console.WriteLine(editResponse.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Edit order was cancelled. Press any key to continue.");
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("An error has occurred");
                Console.WriteLine(orderDateResponse.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}








