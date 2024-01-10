using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public OrderController(SupermarketDbContext context, IMemoryCache cache)
        {
            _context = context;
            _memoryCache = cache;
        }

        public async Task<IActionResult> Index()
        {
            var id = _memoryCache.Get<int>("customerId");
            var supermarketDbContext = _context.Order.Include(x => x.UserOrders)
                                            .Include("UserOrders.Product")
                                            .Include("UserOrders.Product.Category")
                                            .Include("Customer")
                                            .Where(x => x.CustomerId == id);
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


        public IActionResult Edit(int? id)
        {
            if (id == null || _context.TakeAwayProduct == null)
            {
                return NotFound();
            }

            List<string> status = new List<string>
            {
                "Preparing",
                "Prepared",
                "Delivered"
            };

            // SelectList'i oluştur ve ViewData'ya ekle
            SelectList selectList = new SelectList(status);
            ViewData["Status"] = selectList;

            var takeAwayProduct = _context.User_Order.Include("Order").FirstOrDefault(x => x.Id == id);
            if (takeAwayProduct == null)
            {
                return NotFound();
            }
            return View(takeAwayProduct);
        }

        [HttpPost]
        public IActionResult Update(int orderId,string Selected)
        {
            var data = _context.Order.Where(x => x.Id == orderId).First();
            if (Selected.Equals("Delivered"))
            {
                data.DeliveryDate = DateTime.Now;
            }
            data.Status = Selected;
            _context.Order.Update(data);
            _context.SaveChanges();
            return RedirectToAction("Index","Order");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var data = _context.Order.Include(x => x.UserOrders)
                                            .Include("UserOrders.Product")
                                            .Include("UserOrders.Product.Category")
                                            .Include("Customer")
                                            .FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.TakeAwayProduct == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Product'  is null.");
            }
            var product = _context.Order.Include(x=>x.UserOrders).FirstOrDefault(x=>x.Id == id);
            if (product != null)
            {
                _context.Order.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var data = _context.Order.Include(x=> x.UserOrders)
                                            .Include("UserOrders.Product")
                                            .Include("UserOrders.Product.Category")
                                            .Include("Customer")
                                            .FirstOrDefault(x => x.Id == id);
            return View(data);
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
