using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Supermarket.Data;
using Supermarket.Models;
using static NuGet.Packaging.PackagingConstants;

namespace Supermarket.Controllers
{
    public class OrderController : Controller
    {
        
        private readonly SupermarketDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public OrderController(SupermarketDbContext context,IMemoryCache cache)
        {
            _context = context;
            _memoryCache = cache;
        }

        public async Task<IActionResult> Index()
        {
            var id = _memoryCache.Get<int>("customerId");
            var supermarketDbContext = _context.User_Order.Include("Order")
                                            .Include("Product")
                                            .Include("Order.Customer")
                                            .Include("Product.Category")
                                            .Where(x=> x.Order.CustomerId == id);
            var orders = await supermarketDbContext.ToListAsync();
            return View(orders);
        }

        // CREATE: Create a new order
        [HttpPost]
        public void CreateOrder(User_Order newOrder)
        {
            // You can create OrderId uniquely
            // Perform any other necessary actions
            // For example, inserting into the database or using another storage mechanism
            _context.User_Order.Add(newOrder);

            Console.WriteLine("Order completed successfully.");
        }

        // SELECT:Get orders as a list
        public async Task<List<User_Order>> GetOrders()
        {
            return await _context.User_Order.ToListAsync();
        }

        // UPDATE: Update order information
        [HttpPost]
        public void UpdateOrder(int orderId, User_Order updatedOrder)
        {
            // find order with orderId
            var existingOrder = _context.User_Order.FirstOrDefault(o => o.Id == orderId);

            if (existingOrder != null)
            {
                // Perform update operations
                existingOrder.Order.CustomerId = updatedOrder.Order.CustomerId;
                existingOrder.Order.DeliveryDate = updatedOrder.Order.DeliveryDate;
                //existingOrder.ProductList = updatedOrder.ProductList;
                existingOrder.Order.Status = updatedOrder.Order.Status;
                existingOrder.Order.TotalPrice = updatedOrder.Order.TotalPrice;

                Console.WriteLine("Sipariş başarıyla güncellendi.");
            }
            else
            {
                Console.WriteLine("Belirtilen orderId'ye sahip sipariş bulunamadı.");
            }
        }

        // DELETE: Delete order with specified orderId
        [HttpPost]
        public void DeleteOrder(int orderId)
        {
            // find order with orderId
            var orderToDelete = _context.User_Order.FirstOrDefault(o => o.Id == orderId);

            if (orderToDelete != null)
            {
                // Remove order from list
                _context.User_Order.Remove(orderToDelete);

                Console.WriteLine("Sipariş başarıyla silindi.");
            }
            else
            {
                Console.WriteLine("Belirtilen orderId'ye sahip sipariş bulunamadı.");
            }
        }

    }
}
