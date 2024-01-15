using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly SupermarketDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

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

                    // Verifique se é uma ação de edição antes de validar campos obrigatórios
                    if (employee.EmployeeId > 0)
                    {
                        // Se for uma edição, valide os campos obrigatórios
                        if (employee.Employee_Birth_Date == null)
                        {
                            ModelState.AddModelError("Employee_Birth_Date", "Birth date is required.");
                        }

                        if (employee.Employee_Admission_Date == null)
                        {
                            ModelState.AddModelError("Employee_Admission_Date", "Admission date is required.");
                        }

                        if (string.IsNullOrEmpty(employee.Employee_NIF))
                        {
                            ModelState.AddModelError("Employee_NIF", "NIF number is required.");
                        }
                    }

                    if (ModelState.ErrorCount > 0)
                    {
                        return View(employee);
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
