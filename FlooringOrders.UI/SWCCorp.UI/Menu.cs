using SWCCorp.UI.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.UI
{
    public static class Menu
    {
        public static void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("*************************************");
                Console.WriteLine("Flooring Program");
                Console.WriteLine("*************************************");
                Console.WriteLine("1. Display Orders");
                Console.WriteLine("2. Add an Order");
                Console.WriteLine("3. Edit an Order");
                Console.WriteLine("4. Remove an Order");

                Console.WriteLine("5. Quit");
                Console.Write("\nEnter selection ");

                string userinput = Console.ReadLine();

                switch(userinput)
                {
                    case "1":
                        OrderLookupWorkflow.Execute();
                        break;
                    case "2":
                        AddAnOrderWorkflow.Execute();
                        break;
                    case "3":
                        EditAnOrderWorkflow.Execute();
                        break;
                    case "4":
                        DeleteAnOrderWorkflow.Execute();
                        break;
                    case "5":
                        return;
                }
            }
        }
    }
}
