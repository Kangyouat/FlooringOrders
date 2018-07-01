using SWCCorp.Data;
using SWCCorp.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.BLL
{
    public class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch(mode)
            {
                case "InMemoryOrderRepo":
                    return new OrderManager(new InMemoryOrderRepo(), new InMemoryProductRepo(), new InMemoryTaxRepo());
                case "FileOrderRepo":
                    return new OrderManager(new FileOrderRepo(Settings.FilePath, Settings.Temp), new ProductFileRepo(Settings.ProductPath), new TaxFileRepo(Settings.TaxPath));
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
