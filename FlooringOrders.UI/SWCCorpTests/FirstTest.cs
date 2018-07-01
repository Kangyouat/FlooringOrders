using NUnit.Framework;
using SWCCorp.BLL;
using SWCCorp.Data;
using SWCCorp.Data.Helpers;
using SWCCorp.Models;
using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorpTests
{

    [TestFixture]
    public class FileOrderRepoTests
    {
        public const string FilePath = @"C:\Repos\kangyoua-thao-individualwork\Datas\SWCCorp\Testing\";
        public const string Temp = @"C:\Repos\kangyoua-thao-individualwork\Datas\SWCCorp\Testing\TempOrders.txt";
        public const string Seed = @"C:\Repos\kangyoua-thao-individualwork\Datas\SWCCorp\Testing\Seed.txt";
        public const string FullPath = FilePath + "Orders_09252020.txt";
        [SetUp]

        public void SetUp()
        {
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }
            File.Copy(Seed, FullPath);

            if (File.Exists(Temp))
            {
                File.Delete(Temp);
            }
            File.Copy(Seed, Temp);
        }

        [Test]

        public void CanLoadFileTestRepo()
        {
            OrderManager manager = new OrderManager(new FileOrderRepo(FilePath, Temp), new ProductFileRepo(Settings.ProductPath), new TaxFileRepo(Settings.TaxPath));
            OrderDateLookupResponse response = manager.OrderLookupDate(new DateTime(2020, 09, 25));

            Assert.IsTrue(response.Success);
            Assert.AreEqual(2, response.ListOfOrders.Count());
        }

        [TestCase("name", "MN", "wood", 200, true)]
        public void CanAddOrderTest(string customerName, string state, string material, decimal area, bool success)
        {
            var productRepo = new ProductFileRepo(Settings.ProductPath);
            var taxRepo = new TaxFileRepo(Settings.TaxPath);

            var product = productRepo.LoadProducts(material);
            var tax = taxRepo.LoadTaxes(state);

            OrderManager manager = new OrderManager(new FileOrderRepo(FilePath, Temp), new ProductFileRepo(Settings.ProductPath), new TaxFileRepo(Settings.TaxPath));

            AddAnOrderResponse addOrder = manager.AddOrder(new DateTime(2020, 09, 25), customerName, tax, product, area, 0);
            manager.AddToOrderRepo(addOrder.AddedOrder);

            var thisOrder = manager.OrderLookupDate(new DateTime(2020, 09, 25));

            Assert.AreEqual(3, thisOrder.ListOfOrders.Count());
            var orderThatIAdded = thisOrder.ListOfOrders.SingleOrDefault(o => o.OrderNumber == addOrder.AddedOrder.OrderNumber);

            Assert.AreEqual(addOrder.AddedOrder.CustomerName, orderThatIAdded.CustomerName);
            Assert.AreEqual(addOrder.AddedOrder.Area, orderThatIAdded.Area);
            Assert.AreEqual(addOrder.AddedOrder.State, orderThatIAdded.State);
        }

        [Test]
        public void CanEditOrderTestRepo()
        {  
            var productRepo = new ProductFileRepo(Settings.ProductPath);
            var taxRepo = new TaxFileRepo(Settings.TaxPath);

            var product = productRepo.LoadProducts("tile");
            var tax = taxRepo.LoadTaxes("MN");

            OrderManager manager = new OrderManager(new FileOrderRepo(FilePath, Temp), productRepo, taxRepo);
            AddAnOrderResponse addOrder = manager.AddOrder(new DateTime(2018, 09, 25), "Name Test", tax, product, 100, 0);
            manager.AddToOrderRepo(addOrder.AddedOrder);

            addOrder.AddedOrder.Area = 250;
            addOrder.AddedOrder.CustomerName = "Edited Name Test";

            manager.EditOrder(addOrder.AddedOrder);

            var orderResponse = manager.OrderLookupDate(addOrder.AddedOrder.OrderDate);
            var editedOrder = orderResponse.ListOfOrders.SingleOrDefault(o => o.OrderNumber.Equals(addOrder.AddedOrder.OrderNumber));

            Assert.IsNotNull(editedOrder);
            Assert.AreEqual(addOrder.AddedOrder.Area, editedOrder.Area);
            Assert.AreEqual(addOrder.AddedOrder.CustomerName, editedOrder.CustomerName);

        }

        [Test]
        public void CanDeleteOrder()
        {
            var productRepo = new ProductFileRepo(Settings.ProductPath);
            var taxRepo = new TaxFileRepo(Settings.TaxPath);

            var product = productRepo.LoadProducts("tile");
            var tax = taxRepo.LoadTaxes("MN");

            OrderManager manager = new OrderManager(new FileOrderRepo(FilePath, Temp), new ProductFileRepo(Settings.ProductPath), new TaxFileRepo(Settings.TaxPath));

            AddAnOrderResponse addResponse = manager.AddOrder(new DateTime(2020, 09, 25), "Name Test", tax, product, 100, 0);

            manager.AddToOrderRepo(addResponse.AddedOrder);

            var thisOrder = manager.OrderLookupDate(new DateTime(2020, 09, 25));
            Assert.AreEqual(3, thisOrder.ListOfOrders.Count());

            manager.DeleteOrder(addResponse.AddedOrder.OrderDate, addResponse.AddedOrder.OrderNumber);

            var afterDeleted = manager.OrderLookupDate(new DateTime(2020, 09, 25));
            Assert.AreEqual(2, afterDeleted.ListOfOrders.Count());
        }
    }
}

