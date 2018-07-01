using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Data
{
    public class FileOrderRepo : IOrderRepository
    {
        private string _folderPath;
        private string _tempPath;

        public FileOrderRepo(string folderPath, string tempPath)
        {
            _folderPath = folderPath;
            _tempPath = tempPath;
        }
        public void CreateOrderFile(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(fullPath))
                    {
                        sw.WriteLine("NumberID,OrderID,CustomerName,State,Product,AreaProvided,TotalProductCost,TotalLaborCost,Tax,TotalPrice");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("An error has occured at" + exception.StackTrace);
                    Console.ReadKey();
                }
            }
        }
        public static void CopySeedFiles(string fullPath, string tempPath)
        {
            File.Delete(fullPath);
            File.Copy(tempPath, fullPath);
        }

        public bool Delete(DateTime orderDate, int orderNumber)
        {
            string fileName = "Orders_" + orderDate.ToString("MMddyyyy") + ".txt";
            string fullPath = Path.Combine(_folderPath, fileName);

            var allOrders = GetAllOrdersActual(orderDate).ToList();
            bool success = false;
            Order orderToRemove = LoadOrder(orderDate, orderNumber);
            if (orderToRemove != null)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(fullPath))
                    using (StreamWriter sw = new StreamWriter(_tempPath))
                    {
                        sr.ReadLine();
                        const string header = "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";
                        sw.WriteLine(header);
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] columns = line.Split(',');
                            if (int.Parse(columns[0]) != orderToRemove.OrderNumber)
                            {
                                sw.WriteLine(line);
                            }
                        }
                        success = true;
                    }
                    CopySeedFiles(fullPath, _tempPath);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("An error has occurred at" + exception.StackTrace);
                    Console.ReadKey();
                }
            }
            return success;
        }

        public void Edit(Order editedOrder)
        {
            var allOrders = GetAllOrdersActual(editedOrder.OrderDate).ToList();

            string fileName = "Orders_" + editedOrder.OrderDate.ToString("MMddyyyy") + ".txt";
            string fullPath = Path.Combine(_folderPath, fileName);

            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                using (StreamWriter sw = new StreamWriter(_tempPath))
                {
                    sr.ReadLine();
                    const string header = "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";
                    sw.WriteLine(header);
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] columns = line.Split(',');
                        if (int.Parse(columns[0]) == editedOrder.OrderNumber)
                        {
                            sw.WriteLine(ConvertOrderToString(editedOrder).ToList());
                        }
                        else
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
                CopySeedFiles(fullPath, _tempPath);
                allOrders.Add(editedOrder);
            }
            catch (Exception exception)
            {
                Console.WriteLine("An error has occurred at" + exception.StackTrace);
                Console.ReadKey();
            }
        }

        public IEnumerable<Order> GetAllOrdersActual(DateTime orderDate)
        {
            List<Order> orders = new List<Order>();

            string fileName = "Orders_" + orderDate.ToString("MMddyyyy") + ".txt";
            string fullPath = Path.Combine(_folderPath, fileName);

            CreateOrderFile(fullPath);
            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                {
                    sr.ReadLine();
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] columns = line.Split(',');

                        var order = new Order();

                        order.OrderNumber = int.Parse(columns[0]);
                        order.CustomerName = columns[1];
                        order.State = columns[2];
                        order.TaxRate = decimal.Parse(columns[3]);
                        order.ProductType = columns[4];
                        order.Area = decimal.Parse(columns[5]);
                        order.CostPerSquareFoot = decimal.Parse(columns[6]);
                        order.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                        order.TaxRate = decimal.Parse(columns[8]);

                        orders.Add(order);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("An error has occurred at" + exception.StackTrace);
                Console.ReadKey();
            }

            return orders;
        }

        public Order LoadOrder(DateTime orderDate, int orderNumber)
        {
            var allOrders = GetAllOrdersActual(orderDate);
            List<Order> orders = new List<Order>();

            string fileName = "Orders_" + orderDate.ToString("MMddyyyy") + ".txt";
            string fullPath = Path.Combine(_folderPath, fileName);

            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                {
                    sr.ReadLine();
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] columns = line.Split(',');

                        var order = new Order();

                        order.OrderNumber = int.Parse(columns[0]);
                        order.CustomerName = columns[1];
                        order.State = columns[2];
                        order.TaxRate = decimal.Parse(columns[3]);
                        order.ProductType = columns[4];
                        order.Area = decimal.Parse(columns[5]);
                        order.CostPerSquareFoot = decimal.Parse(columns[6]);
                        order.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                        order.TaxRate = decimal.Parse(columns[8]);

                        orders.Add(order);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("An error has occurred at" + exception.StackTrace);
                Console.ReadKey();
            }
            return allOrders.SingleOrDefault(f => f.OrderNumber == orderNumber);
        }

        public void SaveOrder(Order toSave)
        {
            var allOrders = GetAllOrdersActual(toSave.OrderDate).ToList();
            var maxOrderOrderNumber = 1;

            string fileName = "Orders_" + toSave.OrderDate.ToString("MMddyyyy") + ".txt";
            string fullPath = Path.Combine(_folderPath, fileName);

            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                using (StreamWriter sw = new StreamWriter(_tempPath))
                {
                    sr.ReadLine();
                    const string header = "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";
                    sw.WriteLine(header);
                    string line;
                    var i = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] columns = line.Split(',');
                        if (int.Parse(columns[0]) == allOrders[i].OrderNumber)
                        {
                            sw.WriteLine(line);
                        }
                        i++;
                    }

                    if (allOrders.Any())
                    {
                        maxOrderOrderNumber = allOrders.Max(n => n.OrderNumber) + 1;
                    }

                    toSave.OrderNumber = maxOrderOrderNumber;
                    sw.WriteLine(ConvertOrderToString(toSave));
                }
                CopySeedFiles(fullPath, _tempPath);

                allOrders.Add(toSave);
            }
            catch (Exception exception)
            {
                Console.WriteLine("An error has occurred at" + exception.StackTrace);
                Console.ReadKey();
            }

        }
        private string ConvertOrderToString(Order toConvert)
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", toConvert.OrderNumber, toConvert.CustomerName,
                toConvert.State, toConvert.TaxRate, toConvert.ProductType, toConvert.Area, toConvert.CostPerSquareFoot,
                toConvert.LaborCostPerSquareFoot, toConvert.MaterialCost, toConvert.LaborCost, toConvert.TotalTax, toConvert.TotalCost);
        }
    }
}


