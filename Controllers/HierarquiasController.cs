using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    //[Authorize]
    public class HierarquiasController : Controller
    {
        private readonly SupermarketDbContext _context;

        public HierarquiasController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Hierarquias
        public async Task<IActionResult> Index()
        {
            if (TempData.ContainsKey("MensagemExclusao"))
            {
                ViewBag.MensagemExclusao = TempData["MensagemExclusao"];
            }

            var hierarquias = await _context.Hierarquias
                .Include(h => h.Superiores)
                .Include(h => h.Subordinados)
                .ToListAsync();

            var employeesViewModel = ObterEmployeesViewModel();

            ViewData["EmployeesViewModel"] = employeesViewModel;

            return View(hierarquias);
        }

        private EmployeesViewModel ObterEmployeesViewModel()
        {
            var employees = _context.Employee.ToList();
            return new EmployeesViewModel { Employees = employees };
        }

        // GET: Hierarquias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hierarquia = await _context.Hierarquias
                .Include(h => h.Superiores)
                .Include(h => h.Subordinados)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hierarquia == null)
            {
                return NotFound();
            }
            if (TempData.ContainsKey("MensagemCriadoSuccess"))
            {
                ViewBag.MensagemCriadoSuccess = TempData["MensagemCriadoSuccess"];
            }
            if (TempData.ContainsKey("MensagemEditadoSuccess"))
            {
                ViewBag.MensagemEditadoSuccess = TempData["MensagemEditadoSuccess"];
            }

            return View(hierarquia);
        }

        // GET: Hierarquias/Create
        public IActionResult Create()
        {
            if (TempData.ContainsKey("MensagemErro"))
            {
                ViewBag.MensagemErro = TempData["MensagemErro"];
            }

            // Passe null para os IDs selecionados para evitar pré-seleção
            PopulateEmployeeDropdowns(null, null);
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SuperiorId,SubordinadoId")] Hierarquias hierarquia)
        {
            // Verifica se já existe uma relação similar
            if (_context.Hierarquias.Any(h => h.SuperiorId == hierarquia.SuperiorId && h.SubordinadoId == hierarquia.SubordinadoId))
            {
                ViewBag.MensagemErro = "Essa relação já existe.";
                PopulateEmployeeDropdowns(null, null);
                return View(hierarquia);
            }

            if (ModelState.IsValid)
            {
                _context.Add(hierarquia);
                await _context.SaveChangesAsync();

                TempData["MensagemCriadoSuccess"] = "Relação criada com sucesso.";

                // Redireciona para a página de detalhes com o ID da hierarquia recém-criada
                return RedirectToAction("Details", new { id = hierarquia.Id });
            }

            // Passe null para os IDs selecionados para evitar pré-seleção
            PopulateEmployeeDropdowns(null, null);
            return View(hierarquia);
        }

        // GET: Hierarquias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hierarquia = await _context.Hierarquias.FindAsync(id);

            if (hierarquia == null)
            {
                return NotFound();
            }
            if (TempData.ContainsKey("MensagemErro"))
            {
                ViewBag.MensagemErro = TempData["MensagemErro"];
            }

            // Passe os IDs selecionados para evitar pré-seleção
            PopulateEmployeeDropdowns(hierarquia.SuperiorId, hierarquia.SubordinadoId);
            return View(hierarquia);
        }


        // POST: Hierarquias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SuperiorId,SubordinadoId")] Hierarquias hierarquia)
        {
            if (id != hierarquia.Id)
            {
                return NotFound();
            }
            if (_context.Hierarquias.Any(h => h.SuperiorId == hierarquia.SuperiorId && h.SubordinadoId == hierarquia.SubordinadoId))
            {
                ViewBag.MensagemErro = "Essa relação já existe.";
                PopulateEmployeeDropdowns(hierarquia.SuperiorId, hierarquia.SubordinadoId);
                return View(hierarquia);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hierarquia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HierarquiaExists(hierarquia.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["MensagemEditadoSuccess"] = "Relação Editada com sucesso.";

                return RedirectToAction("Details", new { id = hierarquia.Id });
            }

            PopulateEmployeeDropdowns();
            return View(hierarquia);
        }

        // GET: Hierarquias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hierarquia = await _context.Hierarquias
                .Include(h => h.Superiores)
                .Include(h => h.Subordinados)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hierarquia == null)
            {
                return NotFound();
            }

            return View(hierarquia);
        }
        // ... (código existente)

        // GET: Hierarquias/DeleteConfirmed/5
        public async Task<IActionResult> DeletedConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hierarquia = await _context.Hierarquias
                .Include(h => h.Superiores)
                .Include(h => h.Subordinados)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hierarquia == null)
            {
                return NotFound();
            }

            return View("DeletedConfirmed", hierarquia);
        }


        // POST: Hierarquias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var hierarquia = await _context.Hierarquias.FindAsync(id);

            if (hierarquia == null)
            {
                return NotFound();
            }
            _context.Hierarquias.Remove(hierarquia);
            await _context.SaveChangesAsync();
            // Redirecionar para a ação DeletedConfirmed
            return View("DeletedConfirmed", hierarquia);
        }

        private bool HierarquiaExists(int id)
        {
            return _context.Hierarquias.Any(e => e.Id == id);
        }

        public IActionResult SearchSubordinados()
        {
            // Carregue a lista de hierarquias para preencher o dropdown
            var hierarquias = _context.Hierarquias
                .Include(h => h.Superiores)
                .Include(h => h.Subordinados)
                .ToList();

            // Remova duplicatas mantendo apenas uma entrada para cada Superior
            var uniqueSuperiores = hierarquias.GroupBy(h => h.SuperiorId)
                .Select(group => group.First())
                .ToList();

            ViewData["Hierarquias"] = uniqueSuperiores; // Passe a lista para a view

            return View();
        }

        [HttpPost]
        public IActionResult SearchSubordinados(int selectedHierarquiaId)
        {
            var superior = _context.Employee
                .FirstOrDefault(e => e.EmployeeId == selectedHierarquiaId);

            ViewData["SuperiorNome"] = superior?.Employee_Name;

            // Obtenha os subordinados do superior selecionado
            var subordinados = _context.Hierarquias
                .Where(h => h.SuperiorId == selectedHierarquiaId)
                .Include(h => h.Subordinados)
                .ToList();

            return View("SearchSubordinadosResult", subordinados);
        }


        public IActionResult SearchSuperiores()
        {
            // Carregue a lista de hierarquias para preencher o dropdown
            var hierarquias = _context.Hierarquias
                .Include(h => h.Superiores)
                .Include(h => h.Subordinados)
                .ToList();

            // Remova duplicatas mantendo apenas uma entrada para cada Subordinado
            var uniqueSubordinados = hierarquias.GroupBy(h => h.SubordinadoId)
                .Select(group => group.First())
                .ToList();

            ViewData["Hierarquias"] = uniqueSubordinados; // Passa a lista para a view

            return View();
        }

        [HttpPost]
        public IActionResult SearchSuperiores(int selectedHierarquiaId)
        {
            var subordinado = _context.Employee
                .FirstOrDefault(e => e.EmployeeId == selectedHierarquiaId);

            ViewData["SubordinadoNome"] = subordinado?.Employee_Name;

            // Obtenha os superiores do subordinado selecionado
            var superiores = _context.Hierarquias
                .Where(h => h.SubordinadoId == selectedHierarquiaId)
                .Include(h => h.Superiores)
                .ToList();

            return View("SearchSuperioresResult", superiores);
        }


        // Modifique o método PopulateEmployeeDropdowns
        private void PopulateEmployeeDropdowns(int? selectedSuperiorId = null, int? selectedSubordinadoId = null)
        {
            // Obtenha todos os funcionários disponíveis
            var allEmployees = _context.Employee.ToList();

            // Remova o funcionário selecionado como superior do dropdown de subordinados
            var subordinadoEmployees = allEmployees.Where(e => e.EmployeeId != selectedSuperiorId).ToList();
            ViewData["SubordinadoId"] = new SelectList(subordinadoEmployees, "EmployeeId", "Employee_Name", selectedSubordinadoId);

            // Remova o funcionário selecionado como subordinado do dropdown de superiores
            var superiorEmployees = allEmployees.Where(e => e.EmployeeId != selectedSubordinadoId).ToList();
            ViewData["SuperiorId"] = new SelectList(superiorEmployees, "EmployeeId", "Employee_Name", selectedSuperiorId);
        }

    }
}
