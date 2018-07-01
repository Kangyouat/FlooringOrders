using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Models.Responses
{
    public class DeleteOrderResponse : Response
    {
        public Order DeletedOrder { get; set; }
    }
}
