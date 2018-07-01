using SWCCorp.BLL;
using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.UI.Workflows
{
    public class OrderLookupWorkflow
    {
        public static void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Lookup an OrderDate");
            Console.WriteLine("*************************************");

            var userDateTimeInput = ConsoleIO.GetDateTimeFromUser();
            OrderDateLookupResponse orderDateResponse = manager.OrderLookupDate(userDateTimeInput);
            if(orderDateResponse.Success)
            {
                foreach(var order in orderDateResponse.ListOfOrders)
                {
                    Console.WriteLine($"Order Number: {order.OrderNumber}, Customer Name: {order.CustomerName}, State {order.State}, Tax Rate: {order.TaxRate}, Area: {order.Area}, Cost Per Square Foot: {order.CostPerSquareFoot}, Labor Cost Per Square Foot: {order.LaborCostPerSquareFoot}, Material Cost: {order.MaterialCost}, Labor Cost: {order.LaborCost}, Tax Total: {order.TotalTax}, Total Cost: {order.TotalCost}");
                }
            }
            else
            {
                Console.WriteLine("An error has occurred");
                Console.WriteLine(orderDateResponse.Message);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
