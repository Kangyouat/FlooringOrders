using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Models.Responses
{
   public class ProductLookupResponse : Response
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
