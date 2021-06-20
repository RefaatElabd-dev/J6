using J6.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.BL.Servises
{
    public interface IOrderServices
    {
        public Task SwitchCartToOrder(int CustomerId);
        public Task approveOrder(int CustomerId);
        public Task StoreTransaction(Order order);
        public void HandleAdminStatus(int OrderId, OrderStatus status);
        public Task<ICollection<Order>> getAllOrders();
    }
}
