using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Models.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrdersActual(DateTime orderDate);
        Order LoadOrder(DateTime orderDate, int orderNumber);
        void SaveOrder(Order toSave); 
        void Edit(Order editedOrder);
        bool Delete(DateTime orderDate, int orderNumber);
        
    }
}
