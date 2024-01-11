using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    //[Authorize]
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

        //[Authorize(Roles = "Administrator")]
        [Authorize(Roles = "Employeer")]
        public async Task<IActionResult> Index(int page = 1, string employee_name = "", string employee_nif = "")
        {
            var employees = from b in _context.Employee select b;

            if (!string.IsNullOrEmpty(employee_name))
            {
                employees = employees.Where(x => x.Employee_Name.Contains(employee_name));
            }

            if (!string.IsNullOrEmpty(employee_nif))
            {
                employees = employees.Where(x => x.Employee_NIF.Contains(employee_nif));
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
                SearchNIF = employee_nif
            };

            return View(vm);
        }


        // GET: Employees/Details/5
        [Authorize(Roles = "Employeer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Departments)
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



        // GET: Employees/Create

        public IActionResult Create()
        {
            ViewData["IDDepartments"] = new SelectList(_context.Departments, "IDDepartments", "NameDepartments");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employeer")]
        public async Task<IActionResult> Create([Bind("EmployeeId,Employee_Name,Employee_Email,Employee_Password,Employee_Phone,Employee_NIF,Employee_Address,Employee_Birth_Date,Employee_Admission_Date,Employee_Termination_Date,Standard_Check_In_Time,Standard_Check_Out_Time,Standard_Lunch_Hour,Standard_Lunch_Time,IDDepartments")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                bool emailExists = await _context.Employee.AnyAsync(b => b.Employee_Email == employee.Employee_Email);
                bool phoneExists = await _context.Employee.AnyAsync(b => b.Employee_Phone == employee.Employee_Phone);
                bool nifExists = await _context.Employee.AnyAsync(b => b.Employee_NIF == employee.Employee_NIF);
                if (emailExists)
                {
                    ModelState.AddModelError("Employee_Email", "Email is already in use.");
                }

                if (phoneExists)
                {
                    ModelState.AddModelError("Employee_Phone", "Phone number is already in use.");
                }

                if (nifExists)
                {
                    ModelState.AddModelError("Employee_NIF", "NIF is already in use.");
                }

                if (emailExists || phoneExists || nifExists)
                {
                    return View(employee);
                }

                _context.Add(employee);
                await _context.SaveChangesAsync();

                return View("Details", employee);
            }
            ViewData["IDDepartments"] = new SelectList(_context.Departments, "IDDepartments", "NameDepartments", employee.IDDepartments);
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
            ViewData["IDDepartments"] = new SelectList(_context.Departments, "IDDepartments", "NameDepartments", employee.IDDepartments);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employeer")]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool emailExists = await _context.Employee.AnyAsync(b => b.Employee_Email == employee.Employee_Email && b.EmployeeId != employee.EmployeeId);
                    bool phoneExists = await _context.Employee.AnyAsync(b => b.Employee_Phone == employee.Employee_Phone && b.EmployeeId != employee.EmployeeId);
                    bool nifExists = await _context.Employee.AnyAsync(b => b.Employee_NIF == employee.Employee_NIF && b.EmployeeId != employee.EmployeeId);

                    if (emailExists)
                    {
                        ModelState.AddModelError("Employee_Email", "Email is already in use.");
                    }

                    if (phoneExists)
                    {
                        ModelState.AddModelError("Employee_Phone", "Phone number is already in use.");
                    }

                    if (nifExists)
                    {
                        ModelState.AddModelError("Employee_NIF", "NIF is already in use.");
                    }
                   
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                    return View("Details", employee);
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
            }
            ViewData["IDDepartments"] = new SelectList(_context.Departments, "IDDepartments", "NameDepartments", employee.IDDepartments);
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
        [Authorize(Roles = "Employeer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Employee'  is null.");
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                try
                {
                    _context.Employee.Remove(employee);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    ViewBag.ErrorDeleting = "The Employee cannot be deleted because it is associated with an Purchase.";
                    return View();
                }

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
