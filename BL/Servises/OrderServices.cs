using J6.DAL.Database;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.BL.Servises
{
    public class OrderServices : IOrderServices
    {
        private readonly DbContainer _context;

        public OrderServices(DbContainer context)
        {
            _context = context;
        }
        public async Task approveOrder(int CustomerId)
        {
            //Delete OrderProducts
            Order order = _context.Orders.Where(O => O.CustimerId == CustomerId).OrderByDescending(O => O.Id).Take(1).First();
            ICollection<ProdOrder> OrderProducts = await _context.ProdOrders.Where(p => p.OrderId == order.Id).ToListAsync();
            
            foreach (var item in OrderProducts)
            {
                Product product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
                product.SoldQuantities += item.quantity == 0 ? 1 : item.quantity;
                _context.Products.Update(product);
                _context.ProdOrders.Remove(item);
                await _context.SaveChangesAsync();
            }
            //AddPaymentTransaction
            await StoreTransaction(order);
        }

        public void HandleAdminStatus(int OrderId, OrderStatus status)
        {
            Order order = _context.Orders.FirstOrDefault(O => O.Id == OrderId);
            order.Status = status;
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public async Task StoreTransaction(Order order)
        {
            var Payment = new Payment
            {
                OrderId = order.Id,
                Cost = order.OrderCost
            };
            await _context.Payments.AddAsync(Payment);
            await _context.SaveChangesAsync();
        }

        public async Task SwitchCartToOrder(int CustomerId)
        {
            //Get Customer Cart
            int CartId = _context.Carts.Where(c => c.CustimerId == CustomerId).Select(c => c.Id).FirstOrDefault();
            if(CartId == 0) throw new Exception("This User Hasn't a cart products yet Pick some products and return again!");
           
            //Get All Customer Cart Products
            ICollection<ProdCart> cartProducts = await _context.ProdCarts.Where(C => C.CartId == CartId).ToListAsync();
            
            //Create New order Record
            Order CustomerOrder = await CreateNewOrderRecord(CustomerId);
            
            //Store Customer Cart Products To Order table
            int orderId = CustomerOrder.Id;
            foreach (var item in cartProducts)
            {
                var ProductOrder = new ProdOrder
                {
                    OrderId = orderId,
                    ProductId = item.ProductId,
                    quantity = item.quantity
                };
                await _context.ProdOrders.AddAsync(ProductOrder);
                await _context.SaveChangesAsync();
                Product product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
                double productCost = product.Price * product.Discount == null ? 1.0 : double.Parse(product.Discount.ToString());
                CustomerOrder.OrderCost += productCost;
            }

            _context.Orders.Update(CustomerOrder);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> CreateNewOrderRecord(int UserId)
        {
            Order order = new Order { CustimerId = UserId, OrderCost = 0};
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return _context.Orders.Where(O=>O.CustimerId == UserId).OrderByDescending(O => O.Id).Take(1).First();
        }

        public async Task<ICollection<Order>> getAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<IEnumerable<Product>> getAllProductsWithCustomerIdAsync(int custommerId)
        {
            int orderId = await _context.Orders.Where(O => O.CustimerId == custommerId).Select(O => O.Id).OrderByDescending(I => I).Take(1).FirstOrDefaultAsync();
            if(orderId != 0)
                return await getAllProductsWithOrderIdAsync(orderId);
            return null;
        }

        public async Task<IEnumerable<Product>> getAllProductsWithOrderIdAsync(int orderId)
        {
            IEnumerable<int> ids = await _context.ProdOrders.Where(O => O.OrderId == orderId).Select(O => O.ProductId).ToListAsync();
            return await _context.Products.Where(P => ids.Contains(P.Id)).ToListAsync();
        }

    }
}
