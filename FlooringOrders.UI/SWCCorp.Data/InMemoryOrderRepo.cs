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
    public class InMemoryOrderRepo : IOrderRepository
    {
        static List<Order> _orders = new List<Order>
        {
            new Order()
            {
            OrderDate = new DateTime(2018, 09, 25),
            OrderNumber = 111,
            CustomerName = "Peter",
            State = "OH",
            TaxRate = 6.25M,
            ProductType = "Wood",
            Area = 100.00M,
            CostPerSquareFoot = 5.15M,
            LaborCostPerSquareFoot = 4.75M,
            },
            new Order()
            {
            OrderDate = new DateTime(2018, 09, 25),
            OrderNumber = 2,
            CustomerName = "Andy",
            State = "OH",
            TaxRate = 6.25M,
            ProductType = "Wood",
            Area = 100.00M,
            CostPerSquareFoot = 5.15M,
            LaborCostPerSquareFoot = 4.75M,
            },
            new Order()
            {
            OrderDate = new DateTime(2018, 09, 25),
            OrderNumber = 3,
            CustomerName = "Lake",
            State = "OH",
            TaxRate = 6.25M,
            ProductType = "Wood",
            Area = 100.00M,
            CostPerSquareFoot = 5.15M,
            LaborCostPerSquareFoot = 4.75M,
            },
            new Order()
            {
            OrderDate = new DateTime(2018, 09, 28),
            OrderNumber = 5,
            CustomerName = "Luke",
            State = "OH",
            TaxRate = 6.25M,
            ProductType = "Wood",
            Area = 100.00M,
            CostPerSquareFoot = 5.15M,
            LaborCostPerSquareFoot = 4.75M,
            },
            new Order()
            {
            OrderDate = new DateTime(2018, 09, 29),
            OrderNumber = 4,
            CustomerName = "Tillie",
            State = "OH",
            TaxRate = 6.25M,
            ProductType = "Wood",
            Area = 100.00M,
            CostPerSquareFoot = 5.15M,
            LaborCostPerSquareFoot = 4.75M,
            }
        };

        public Order Add(Order toAddOrderFile)
        {
            throw new NotImplementedException();
        }

        public bool Delete(DateTime orderDate, int orderNumber)
        {
            bool success = false;

            Order toRemove = LoadOrder(orderDate, orderNumber);
            if (toRemove != null)
            {
                _orders.Remove(toRemove);
                success = true;
            }
            return success;
        }

        public void Edit(Order editedOrder)
        {
            Order existing = LoadOrder(editedOrder.OrderDate, editedOrder.OrderNumber);

            if (existing != null)
            {
                _orders.Remove(existing);
                _orders.Add(editedOrder);
            }
        }

        public IEnumerable<Order> GetAllOrdersActual(DateTime orderDate)
        {
            var getOrderDate = _orders.Where(o => o.OrderDate == orderDate);
            return getOrderDate;
        }

        public Order LoadOrder(DateTime orderDate, int orderNumber)
        {
            return _orders.Where(o => o.OrderDate == orderDate).SingleOrDefault(f => f.OrderNumber == orderNumber);
        }

        public void SaveOrder(Order toSave)
        {
            toSave.OrderNumber = _orders.Count + 1;
            _orders.Add(toSave);
        }
    }
}
