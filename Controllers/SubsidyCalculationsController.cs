using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class SubsidyCalculationsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public SubsidyCalculationsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: SubsidyCalculations
        /*
        public async Task<IActionResult> Index(int page = 1)
        {
            // Obtém todos os pontos sem modificar a lista completa
           var allPoints = await _context.Ponto.Include(p => p.Employee).ToListAsync();
        
            var date = _context.Ponto.AsQueryable();  // Certifique-se de que 'date' seja IQueryable<Ponto>

            PagingInfo paging = new PagingInfo
            {
                CurrentPage = page,
                TotalItems = await date.CountAsync(),  // Agora, 'CountAsync' deve funcionar corretamente
            };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }

            var vm = new SubsidyCalculationViewModel
            {
                Pontos = await date
                    .OrderBy(x => x.Date)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),  // Agora, 'ToListAsync' deve funcionar corretamente
                PagingInfo = paging,
             ////   SearchName = "",
            };


            
         

            return View(vm);
        }


        


        public async Task<IActionResult> Index(int page = 1, string employee_name = "")
        {
            var test = from b in _context.SubsidyCalculation select b;

            if (!string.IsNullOrEmpty(employee_name))
            {
                test = test.Where(x => x.Ponto.Employee.Employee_Name.Contains(employee_name));
            }

            PagingInfo paging = new PagingInfo
            {
                CurrentPage = page,
                TotalItems = await test.CountAsync(),
            };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }

            var vm = new SubsidyCalculationViewModel
            {
                SubsidyCalculations = await test
                    .OrderBy(b => b.Ponto.Employee.Employee_Name)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),
                PagingInfo = paging,
                SearchName = employee_name,
            };

            return View(vm);
        }



        public async Task<IActionResult> Index(int page = 1, string SearchName = "")
        {



            var pontos = from b in _context.Ponto select b;
            /*
            if (SearchName != "")
            {
                pontos = pontos.Where(x => x.Ponto.Employee.Employee_Name.Contains(SearchName));
            }

          

            PagingInfo paging = new PagingInfo
            {
                CurrentPage = page,
                TotalItems = await pontos.CountAsync(),
            };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }

            var vm = new SubsidyCalculationViewModel
            {
                SubsidyCalculations = await pontos
                    //.OrderBy(b => b.Ponto.Employee.Employee_Name)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),
                PagingInfo = paging,
                SearchName = SearchName,
              //  SearchAuthor = author,
            };

            return View(vm);
        }
*/

        public async Task<IActionResult> Index(int page = 1, string employee_name="")
        {
            // Obtém todos os pontos sem modificar a lista completa
            var allPoints = await _context.Ponto.Include(p => p.Employee).ToListAsync();

            var employees = _context.Ponto.AsQueryable();  // Certifique-se de que 'date' seja IQueryable<Ponto>

            if (!string.IsNullOrEmpty(employee_name))
            {
                employees = employees.Where(x => x.Employee.Employee_Name.Contains(employee_name));
            }


            PagingInfo paging = new PagingInfo
            {
                CurrentPage = page,
                TotalItems = await employees.CountAsync(),  // Agora, 'CountAsync' deve funcionar corretamente
            };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }

            var vm = new SubsidyCalculationViewModel
            {
                Pontos = await employees
                    .OrderBy(x => x.Employee.Employee_Name)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),  // Agora, 'ToListAsync' deve funcionar corretamente
                PagingInfo = paging,
                SearchName = employee_name,
            };

            // Calcular as horas extras para cada Ponto no ViewModel

            return View(vm);
        }



        // GET: SubsidyCalculations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SubsidyCalculation == null)
            {
                return NotFound();
            }

            var subsidyCalculation = await _context.SubsidyCalculation
                .Include(s => s.Ponto)
                //.Include(s => s.SubsidySetup)
                .FirstOrDefaultAsync(m => m.SubsidyCalculationId == id);
            if (subsidyCalculation == null)
            {
                return NotFound();
            }

            return View(subsidyCalculation);
        }

        // GET: SubsidyCalculations/Create
        public IActionResult Create()
        {
            ViewData["PontoId"] = new SelectList(_context.Ponto, "PontoId", "Status");
            ViewData["SubsidySetupId"] = new SelectList(_context.SubsidySetup, "SubsidySetupId", "SubsidySetupId");
            return View();
        }

        // POST: SubsidyCalculations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubsidyCalculationId,TotalHoras,PontoId,SubsidySetupId")] SubsidyCalculation subsidyCalculation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subsidyCalculation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PontoId"] = new SelectList(_context.Ponto, "PontoId", "Status", subsidyCalculation.PontoId);
            //  ViewData["SubsidySetupId"] = new SelectList(_context.SubsidySetup, "SubsidySetupId", "SubsidySetupId", subsidyCalculation.SubsidySetupId);
            return View(subsidyCalculation);
        }

        // GET: SubsidyCalculations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SubsidyCalculation == null)
            {
                return NotFound();
            }

            var subsidyCalculation = await _context.SubsidyCalculation.FindAsync(id);
            if (subsidyCalculation == null)
            {
                return NotFound();
            }
            ViewData["PontoId"] = new SelectList(_context.Ponto, "PontoId", "Status", subsidyCalculation.PontoId);
            //  ViewData["SubsidySetupId"] = new SelectList(_context.SubsidySetup, "SubsidySetupId", "SubsidySetupId", subsidyCalculation.SubsidySetupId);
            return View(subsidyCalculation);
        }

        // POST: SubsidyCalculations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubsidyCalculationId,TotalHoras,PontoId,SubsidySetupId")] SubsidyCalculation subsidyCalculation)
        {
            if (id != subsidyCalculation.SubsidyCalculationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subsidyCalculation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubsidyCalculationExists(subsidyCalculation.SubsidyCalculationId))
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
            ViewData["PontoId"] = new SelectList(_context.Ponto, "PontoId", "Status", subsidyCalculation.PontoId);
            //ViewData["SubsidySetupId"] = new SelectList(_context.SubsidySetup, "SubsidySetupId", "SubsidySetupId", subsidyCalculation.SubsidySetupId);
            return View(subsidyCalculation);
        }

        // GET: SubsidyCalculations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SubsidyCalculation == null)
            {
                return NotFound();
            }

            var subsidyCalculation = await _context.SubsidyCalculation
                .Include(s => s.Ponto)
                // .Include(s => s.SubsidySetup)
                .FirstOrDefaultAsync(m => m.SubsidyCalculationId == id);
            if (subsidyCalculation == null)
            {
                return NotFound();
            }

            return View(subsidyCalculation);
        }

        // POST: SubsidyCalculations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SubsidyCalculation == null)
            {
                return Problem("Entity set 'SupermarketDbContext.SubsidyCalculation'  is null.");
            }
            var subsidyCalculation = await _context.SubsidyCalculation.FindAsync(id);
            if (subsidyCalculation != null)
            {
                _context.SubsidyCalculation.Remove(subsidyCalculation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubsidyCalculationExists(int id)
        {
            return (_context.SubsidyCalculation?.Any(e => e.SubsidyCalculationId == id)).GetValueOrDefault();
        }
    }
}
