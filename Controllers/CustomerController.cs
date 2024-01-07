using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    
    public class CustomerController : Controller
    {
        private readonly SupermarketDbContext _context;
        private readonly ILogger<CustomerController> _logger;
        private readonly IMemoryCache _memoryCache;

        public CustomerController(SupermarketDbContext context, ILogger<CustomerController> logger,IMemoryCache cache)
        {
            _context = context;
            _logger = logger;
            _memoryCache = cache;
        }

        public IActionResult Customer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            try
            {
                if (customer.CustomerId != 0)
                {
                    return Redirect("/Customer/Customer");
                }
                 _context.Customers.Add(customer);
                _context.SaveChanges();
                return Redirect("/Customer/LoginCustomer");
            }
            catch (Exception ex)
            {
                _logger.LogError("Customer Controller +"+ex);
                return Redirect("/Customer/Customer");
            }

        }

        [HttpPost]
        public IActionResult CustomerLogin(string email,string password)
        {
            var customer = _context.Customers.FirstOrDefault(c =>c.CustomerEmail.Equals(email) 
             &&  c.Password.Equals(password));
            

            if (customer != null)
            {
                _memoryCache.Set("customerId", customer.CustomerId);
                return Redirect("/Customer/CustomerList");
            }
            return Redirect("/Customer/LoginCustomer");
        }

        public IActionResult LoginCustomer()
        {
           
            return View();
        }

      


        public IActionResult CustomerList()
        {
            var customers = _context.Customers.ToList();
            return View(customers);
        }

        public IActionResult CustomerDetail(int id) {

            var customer =  _context.Customers.FirstOrDefault(x => x.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        public IActionResult UpdateCustomerDetail(int id)
        {

            var customer = _context.Customers.FirstOrDefault(x => x.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult UpdateCustomer(Customer customer)
        {
            try
            {
                if (customer.CustomerId <1)
                {
                    return CustomerList();
                }
                var existingCustomer = _context.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);

                if (existingCustomer == null)
                {
                    return NotFound();
                }

                existingCustomer.CustomerName = customer.CustomerName;
                existingCustomer.CustomerPhone = customer.CustomerPhone;
                existingCustomer.CustomerEmail = customer.CustomerEmail;
                _context.Update(customer);
                _context.SaveChanges();

                return Redirect("/Customer/CustomerList");
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateCustomer Error: " + ex);
                return Redirect("/Customer/CustomerList");
            }
        }

        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.FirstOrDefault(x=> x.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            _context.Remove(customer);
            _context.SaveChanges();
            return Redirect("/Customer/CustomerList");
        }

        public IActionResult TopCustomers()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetTopCustomersForProductInTime(string productName, DateTime startTime, DateTime endTime)
        {
            try
            {
                

                var orderCustomer = _context.Customers
                .Include(c => c.Orders)
                 .ThenInclude(o => o.Products)
                   .Where(c => c.Orders.Any(o => o.Products.Any(p => p.ProductName.Equals(productName))
                     && o.OrderDate >= startTime && o.OrderDate <= endTime))
                 .ToList();

                return View(orderCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError("Customer Error: " + ex);
                return RedirectToAction("Error");
            }
            
        }


    }
}
