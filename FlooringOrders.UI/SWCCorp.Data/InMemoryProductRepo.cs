using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Data
{
    public class InMemoryProductRepo : IProductRepo
    {

        List<Product> _getProducts = new List<Product>
        {
            new Product
            {
                Name = "tile",
                MaterialUnitCost = 4.00M,
                LaborUnitCost = 6.00M

            },
            new Product
            {
                Name = "wood",
                MaterialUnitCost = 4.50M,
                LaborUnitCost = 6.25M

            },
            new Product
            {
                Name = "carpet",
                MaterialUnitCost = 3.00M,
                LaborUnitCost = 5.00M
            },
        };

        public Product LoadProducts(string productType)
        {
            var product = _getProducts.SingleOrDefault(t => t.Name == productType);
            return product;
        }

        IEnumerable<Product> IProductRepo.GetProducts()
        {
            return _getProducts;
        }

    }
}
