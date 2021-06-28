using J6.DAL.Entities;
using J6.Models;
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
        public void HandleAdminStatus(int OrderId, int statusNumber);
        public Task<ICollection<Order>> getAllOrders();

        //#################New#####################
        public Task<IEnumerable<Product>> getAllProductsWithOrderIdAsync(int orderId);
        public Task<IEnumerable<Product>> getAllProductsWithCustomerIdAsync(int custommerId);
        public Task<IEnumerable<OrderWithProducts>> getOrderProductsInStatusAsync(int customerId, int statusNumber);
    }
}
