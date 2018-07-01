using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Models.Responses
{
    public class AddAnOrderResponse : Response
    {
        public Order AddedOrder { get; set; }
        public Tax StateTax { get; set; }
        public Product ProductType { get; set; }
    }
}
