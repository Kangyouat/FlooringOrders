using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Models.Responses
{
    public class TaxLookupResponse : Response
    {
        public IEnumerable<Tax> Taxes { get; set; }
    }
}
