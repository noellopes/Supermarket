using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly SupermarketDbContext _context;

        public EmployeesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        /*
        public async Task<IActionResult> Index()
        {
              return _context.Employee != null ? 
                          View(await _context.Employee.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.Employee'  is null.");
        }
        */
        public async Task<IActionResult> Index(int page = 1, string employee_name = "")
        {
            var employees = from b in _context.Employee select b;

            if (!string.IsNullOrEmpty(employee_name))
            {
                employees = employees.Where(x => x.Employee_Name.Contains(employee_name));
            }

            PagingInfo paging = new PagingInfo
            {
                CurrentPage = page,
                TotalItems = await employees.CountAsync(),
            };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }

            var vm = new EmployeesViewModel
            {
                Employees = await employees
                    .OrderBy(b => b.Employee_Name)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),
                PagingInfo = paging,
                SearchName = employee_name,
            };

            return View(vm);
        }


        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            CalculateWorkingHours(employee);

            return View(employee);
        }

        private void CalculateWorkingHours(Employee employee)
        {
            if (!string.IsNullOrEmpty(employee.Standard_Check_In_Time) && !string.IsNullOrEmpty(employee.Standard_Check_Out_Time))
            {
                DateTime inTime = DateTime.Parse(employee.Standard_Check_In_Time);
                DateTime outTime = DateTime.Parse(employee.Standard_Check_Out_Time);

                if (outTime > inTime)
                {
                    TimeSpan workingHours = outTime - inTime;
                    employee.Employee_Time_Bank = workingHours;
                }
                else
                {
                    employee.Employee_Time_Bank = TimeSpan.Zero;
                }
            }
        }



        private async Task<bool> IsEmailUnique(string email)
        {
            return await _context.Employee.AllAsync(e => e.Employee_Email != email);
        }
        private async Task<bool> IsPhoneUnique(string phone)
        {
            return await _context.Employee.AllAsync(e => e.Employee_Phone != phone);
        }

        private async Task<bool> IsNIFUnique(string nif)
        {
            return await _context.Employee.AllAsync(e => e.Employee_NIF != nif);
        }


        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Employee_Name,Employee_Email,Employee_Password,Employee_Phone,Employee_NIF,Employee_Address,Employee_Birth_Date,Employee_Admission_Date,Employee_Termination_Date,Standard_Check_In_Time,Standard_Check_Out_Time,Standard_Lunch_Hour,Standard_Lunch_Time")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (!await IsEmailUnique(employee.Employee_Email))
                {
                    ModelState.AddModelError("Employee_Email", "Email is already in use.");
                    return View(employee);
                }

                if (!await IsPhoneUnique(employee.Employee_Phone))
                {
                    ModelState.AddModelError("Employee_Phone", "Phone number is already in use.");
                    return View(employee);
                }

                if (!await IsNIFUnique(employee.Employee_NIF))
                {
                    ModelState.AddModelError("Employee_NIF", "NIF is already in use.");
                    return View(employee);
                }

                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,Employee_Name,Employee_Email,Employee_Password,Employee_Phone,Employee_NIF,Employee_Address,Employee_Birth_Date,Employee_Admission_Date,Employee_Termination_Date,Standard_Check_In_Time,Standard_Check_Out_Time,Standard_Lunch_Hour,Standard_Lunch_Time,Employee_Time_Bank")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Employee'  is null.");
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employee?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
