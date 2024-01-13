using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{

    [Authorize]
    public class FolgasController : Controller
    {
        private readonly SupermarketDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public FolgasController(SupermarketDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Folgas
        [Authorize]
        public async Task<IActionResult> Index(int? page, DateTime? dataPedido, int? employeeId)
        {
            int pageSize = 100;
            int pageNumber = page ?? 1;

            var query = _context.Folga.Include(f => f.Employee).AsQueryable();







            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewData["PageIndex"] = pageNumber;
            ViewData["TotalPages"] = totalPages;
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name");
            var user = await _userManager.GetUserAsync(User);

            if (User.IsInRole("Funcionário"))
            {
                var employee = _context.Employee.FirstOrDefault(e => e.Employee_Email == user.UserName);

                if (employee == null)
                {
                    ModelState.AddModelError(string.Empty, "Funcionário não encontrado");
                    return View();
                }

                ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;


                var queryData = _context.Folga.Where(f => f.EmployeeId == employee.EmployeeId).Include(f => f.Employee).AsQueryable();

                if (dataPedido.HasValue)
                {
                    queryData = queryData.Where(f => f.DataPedido.Date == dataPedido.Value.Date);
                }
                var folgasFuncionario = await queryData.ToListAsync();
                if (folgasFuncionario.Count == 0)
                {
                    ViewData["Message"] = "Funcionário sem datas nesse período";
                }
                return View(folgasFuncionario);

            }
            else if (User.IsInRole("Gestor"))
            {
                var folgas = await _context.Folga.Include(f => f.Employee).ToListAsync();

                if (employeeId.HasValue)
                {
                    query = _context.Folga.Where(f => f.EmployeeId == employeeId);
                }



                var queryList = await query.ToListAsync();

                if (queryList.Count == 0)
                {
                    ViewData["Message"] = "Funcionário sem folgas solicitadas";
                }
                return View(queryList);


            }
            return View(items);
        }














        [Authorize(Roles = "Funcionário")]
        public IActionResult FolgasFuncionario()
        {
            return View();
        }





        [Authorize(Roles = "Funcionário")]
        // GET: Folgas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Folga == null)
            {
                return NotFound();
            }

            var folga = await _context.Folga
                .Include(f => f.Employee)
                .FirstOrDefaultAsync(m => m.FolgaId == id);
            if (folga == null)
            {
                return NotFound();
            }

            return View(folga);
        }

        // GET: Folgas/Create
        [Authorize(Roles = "Funcionário")]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Utilizador não autenticado");
                return View();
            }

            var employee = _context.Employee.FirstOrDefault(e => e.Employee_Email == user.UserName);

            if (employee == null)
            {
                ModelState.AddModelError(string.Empty, "Empregado não encontrado");
                return View();
            }
            var folga = new Folga
            {
                EmployeeId = employee.EmployeeId
            };

            ViewData["EmployeeId"] = new SelectList(_context.Employee.Where(e => e.EmployeeId == employee.EmployeeId), "EmployeeId", "Employee_Name");
            return View(folga);
        }

        // POST: Folgas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FolgaId,EmployeeId,GestorId,Status,DataPedido,DataResultado,DataInicio,DataFim,motivo")] Folga folga)
        {
            if (ModelState.IsValid)
            {
                folga.Status = Folga.FolgaStatus.Pendente;//Quando a folga for inserida na base de dados muda o estado para "Pendente"
                _context.Add(folga);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "A sua folga foi adicionada com sucesso";
                return RedirectToAction(nameof(Index));

            }

            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", folga.EmployeeId);


            return View(folga);
        }

        // GET: Folgas/Edit/5

        [Authorize(Roles = "Funcionário")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Folga == null)
            {
                return NotFound();
            }

            var folga = await _context.Folga.FindAsync(id);
            if (folga == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);

            var employee = _context.Employee.FirstOrDefault(e => e.Employee_Email == user.UserName);

            if (employee == null || folga.EmployeeId != employee.EmployeeId)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee.Where(e => e.EmployeeId == employee.EmployeeId), "EmployeeId", "Employee_Name");
            return View(folga);
        }

        // POST: Folgas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FolgaId,EmployeeId,GestorId,Status,DataPedido,DataResultado,DataInicio,DataFim,motivo")] Folga folga)
        {
            if (id != folga.FolgaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    folga.Status = Folga.FolgaStatus.Pendente;//Quando a folga for inserida na base de dados muda o estado para "Pendente
                    _context.Update(folga);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "A sua folga foi alterada com sucesso";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FolgaExists(folga.FolgaId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", folga.EmployeeId);
            return View(folga);
        }

        // GET: Folgas/Delete/5
        [Authorize(Roles = "Funcionário")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Folga == null)
            {
                return NotFound();
            }

            var folga = await _context.Folga
                .Include(f => f.Employee)
                .FirstOrDefaultAsync(m => m.FolgaId == id);
            if (folga == null)
            {
                return NotFound();
            }

            return View(folga);
        }

        // POST: Folgas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Folga == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Folga'  is null.");
            }
            var folga = await _context.Folga.FindAsync(id);
            if (folga != null)
            {
                _context.Folga.Remove(folga);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "A sua folga foi eliminada com sucesso";
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> FolgasPendentes()
        {
            var folgasPendentes = await _context.Folga.Where(f => f.Status == Folga.FolgaStatus.Pendente).Include(f => f.Employee).ToListAsync();
            return View(folgasPendentes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> AprovarFolga(int id, string aprovacao, int GestorId, DateTime DataResultado)
        {
            var folga = await _context.Folga.Include(f => f.Employee).FirstOrDefaultAsync(f => f.FolgaId == id);

            if (folga != null)
            {
                if (aprovacao == "Aprovada")
                {


                    folga.Status = Folga.FolgaStatus.Aprovada;
                    folga.GestorId = GestorId;
                    folga.DataResultado = DataResultado;

                    await _context.SaveChangesAsync();





                }
                else if (aprovacao == "Rejeitada")
                {
                    folga.Status = Folga.FolgaStatus.Rejeitada;
                    folga.GestorId = GestorId;
                    folga.DataResultado = DataResultado;




                    {
                        TempData["SuccessMessage"] = "Folga Rejeitada";
                    }
                }
                await _context.SaveChangesAsync();


                TempData["EmployeeId"] = folga.EmployeeId;
                return RedirectToAction(nameof(FolgasPendentes));




            }
            return RedirectToAction(nameof(FolgasPendentes));
        }



        [Authorize(Roles = "Funcionário")]
        public async Task<IActionResult> FolgasAprovadas(int funcionarioId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Utilizador não encontrado");
                return View();
            }
            var employee = _context.Employee.FirstOrDefault(e => e.Employee_Email == user.UserName);

            if (employee == null)
            {
                ModelState.AddModelError(string.Empty, "Funcionário não encontrado");
                return View();
            }

            var folgasAprovadas = await _context.Folga.Where(f => f.Status == Folga.FolgaStatus.Aprovada && f.EmployeeId == employee.EmployeeId).Include(f => f.Employee).ToListAsync();
            return View(folgasAprovadas);
        }



        private bool FolgaExists(int id)
        {
            return (_context.Folga?.Any(e => e.FolgaId == id)).GetValueOrDefault();
        }
    }
}
