using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Data
{
    public class InMemoryTaxRepo : ITaxRepo
    {
        List<Tax> _getTaxes = new List<Tax>()
        {
            new Tax
            {
                Abbreviation = "MN",
                TaxRate = 6.85M
            },
            new Tax
            {
                Abbreviation = "NC",
                TaxRate = 5.75M
            },
        };

        public Tax LoadTaxes(string state)
        {
            var tax = _getTaxes.SingleOrDefault(t => t.Abbreviation == state);
            return tax;
        }

        IEnumerable<Tax> ITaxRepo.GetTaxes()
        {
            return _getTaxes;
        }
    }
}
