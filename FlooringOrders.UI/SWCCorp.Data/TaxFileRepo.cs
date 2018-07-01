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
    public class TaxFileRepo : ITaxRepo
    {
        private string _taxPath;
        List<Tax> listOfTaxesState = new List<Tax>();

        public TaxFileRepo(string taxPath)
        {
            _taxPath = taxPath;
            ListOfTaxes();
        }
        public void ListOfTaxes()
        {
            using (StreamReader sr = new StreamReader(_taxPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] columns = line.Split(',');
                    Tax tax = new Tax();

                    tax.Abbreviation = columns[0].ToUpper();
                    tax.TaxRate = decimal.Parse(columns[1]);

                    listOfTaxesState.Add(tax);
                }
            }
        }
        public IEnumerable<Tax> GetTaxes()
        {
            return listOfTaxesState;
        }

        public Tax LoadTaxes(string state)
        {
            var tax = listOfTaxesState.SingleOrDefault(t => t.Abbreviation == state);
            return tax;
        }
    }
}
