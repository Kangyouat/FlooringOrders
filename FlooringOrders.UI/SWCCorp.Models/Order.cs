using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Models
{
    public class Order
    {
        //Identifier fields
        public DateTime OrderDate { get; set; }
        public int OrderNumber { get; set; }

        public string CustomerName { get; set; }
        public string State { get; set; }
        public decimal TaxRate { get; set; }
        public string ProductType { get; set; }
        public decimal Area { get; set; }
        public decimal CostPerSquareFoot { get; set; }
        public decimal LaborCostPerSquareFoot { get; set; }

        //Calculated fields
        public decimal MaterialCost => Area * CostPerSquareFoot;
        public decimal LaborCost => Area * LaborCostPerSquareFoot;
        public decimal TotalTax => (MaterialCost + LaborCost) * TaxRate / 100;
        public decimal TotalCost => MaterialCost + LaborCost + TotalTax;

        public Order()
        {

        }
      
        public Order(Order that)
        {
            this.OrderDate = that.OrderDate;
            this.OrderNumber = that.OrderNumber;
            this.CustomerName = that.CustomerName;
            this.State = that.State;
            this.TaxRate = that.TaxRate;
            this.ProductType = that.ProductType;
            this.Area = that.Area;
            this.CostPerSquareFoot = that.CostPerSquareFoot;
            this.LaborCostPerSquareFoot = that.LaborCostPerSquareFoot;
        }
    }
}
