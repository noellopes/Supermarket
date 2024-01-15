using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Hierarquias.Controllers
{
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

            return View(await _context.Hierarquias.ToListAsync());
        }

        // GET: Hierarquias/Detalhes
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Hierarquia = await _context.Hierarquias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Hierarquia == null)
            {
                return RedirectToAction("Error");
            }

            if (TempData.ContainsKey("MensagemCriadoSuccess"))
            {
                ViewBag.MensagemCriadoSuccess = TempData["MensagemCriadoSuccess"];
            }

            return View(Hierarquia);
        }

        // GET: Hierarquias/Criar
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hierarquias/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] HierarquiasModel Hierarquia)
        {
            if (ModelState.IsValid)
            {
                // Verificar se o nome do Hierarquia já existe
                if (_context.Hierarquias.Any(c => c.Nome == Hierarquia.Nome))
                {
                    ModelState.AddModelError("Nome", "Já existe um Hierarquia com este nome.");
                    return View(Hierarquia);
                }

                _context.Add(Hierarquia);
                await _context.SaveChangesAsync();

                TempData["MensagemCriadoSuccess"] = "Hierarquia criado com sucesso!";

                return RedirectToAction(nameof(Details), new { id = Hierarquia.Id });
            }

            return View(Hierarquia);
        }

        // GET: Hierarquias/Editar
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Hierarquia = await _context.Hierarquias.FindAsync(id);
            if (Hierarquia == null)
            {
                return RedirectToAction("Error");
            }
            return View(Hierarquia);
        }

        // POST: Hierarquias/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] HierarquiasModel Hierarquia)
        {
            if (id != Hierarquia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Verificar se o nome do Hierarquia já existe (exceto para o Hierarquia sendo editado)
                if (_context.Hierarquias.Any(c => c.Nome == Hierarquia.Nome && c.Id != Hierarquia.Id))
                {
                    ModelState.AddModelError("Nome", "Já existe um Hierarquia com este nome.");
                    return View(Hierarquia);
                }

                try
                {
                    _context.Update(Hierarquia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HierarquiaExists(Hierarquia.Id))
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
            return View(Hierarquia);
        }

        // GET: Hierarquias/Deletar
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Hierarquia = await _context.Hierarquias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Hierarquia == null)
            {
                return NotFound();
            }

            return View(Hierarquia);
        }

        // POST: Hierarquias/Deletar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Hierarquias = await _context.Hierarquias.FindAsync(id);
            if (Hierarquias == null)
            {
                return NotFound();
            }

            _context.Hierarquias.Remove(Hierarquias);
            await _context.SaveChangesAsync();

            TempData["MensagemExclusao"] = "Hierarquia excluído com sucesso.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ItemNaoEncontrado()
        {
            return View();
        }

        private bool HierarquiaExists(int id)
        {
            return _context.Hierarquias.Any(e => e.Id == id);
        }
    }
}