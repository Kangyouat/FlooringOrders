using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Models.Responses
{
    public class OrderDateLookupResponse : Response
    {
        public DateTime OrderDate { get; set; }
        public IEnumerable<Order> ListOfOrders { get; set; }
    }
}
