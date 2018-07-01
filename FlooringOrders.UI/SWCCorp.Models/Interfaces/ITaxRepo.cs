using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Models.Interfaces
{
    public interface ITaxRepo
    {
        IEnumerable<Tax> GetTaxes();
        Tax LoadTaxes(string state);
    }
}
