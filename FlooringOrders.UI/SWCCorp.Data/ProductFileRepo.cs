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
    public class ProductFileRepo : IProductRepo
    {
        private string _productPath;
        List<Product> listOfProducts = new List<Product>();

        public ProductFileRepo(string productPath)
        {
            _productPath = productPath;
            ListOfStates();
        }
        public void ListOfStates()
        {
            using (StreamReader sr = new StreamReader(_productPath))
            { 
                string line;
                while((line = sr.ReadLine()) !=null)
                {
                    string[] columns = line.Split(',');
                    Product product = new Product();

                    product.Name = columns[0].ToLower();
                    product.MaterialUnitCost = decimal.Parse(columns[1]);
                    product.LaborUnitCost = decimal.Parse(columns[2]);

                    listOfProducts.Add(product);
                }
            }
        }
        public IEnumerable<Product> GetProducts()
        { 
            return listOfProducts;
        }

        public Product LoadProducts(string productType)
        {
            var product = listOfProducts.SingleOrDefault(t => t.Name == productType);
            return product;
        }
    }
}
