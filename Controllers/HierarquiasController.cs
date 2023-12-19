using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket.Models;
using Supermarket.Data;

namespace Hierarquias.Controllers
{
    public class HierarquiasController : Controller
    {
        private readonly SupermarketDbContext _context;

        public HierarquiasController(SupermarketDbContext context)
        {
            _context = context;
        }

        public IActionResult AllHierarquias()
        {
            var allHierarquias = _context.Hierarquias.ToList();
            return View(allHierarquias);
        }

        // GET: Hierarquias
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employee
        .Include(e => e.Hierarquia)
        .Include(e => e.Funcao)
        .Select(e => new EmployeeViewModel
        {
            EmployeeId = e.EmployeeId,
            EmployeeName = e.Employee_Name,
            HierarquiaNome = e.Hierarquia != null ? e.Hierarquia.HierarquiaNome : "Sem Hierarquia",
            NivelHierarquia = e.Hierarquia != null ? e.Hierarquia.NivelHierarquico : 0,
            FuncaoNome = e.Funcao != null ? e.Funcao.NomeFuncao : "Sem Função"
        })
        .ToListAsync();
            if (TempData.ContainsKey("MensagemExclusao"))
            {
                ViewBag.MensagemExclusao = TempData["MensagemExclusao"];
            }
            var hierarquias = await _context.Hierarquias.OrderBy(h => h.NivelHierarquico).ToListAsync();

            return View(employees);
        }

        // GET: Hierarquias/Detalhes
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Hierarquia = await _context.Hierarquias
                .FirstOrDefaultAsync(m => m.HierarquiaId == id);
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
        public async Task<IActionResult> Create([Bind("Id,Nome,NivelHierarquia")] HierarquiasModel Hierarquia)
        {
            if (ModelState.IsValid)
            {
                // Verificar se o nome do Hierarquia já existe
                if (_context.Hierarquias.Any(c => c.HierarquiaNome == Hierarquia.HierarquiaNome))
                {
                    ModelState.AddModelError("Nome", "Já existe um Hierarquia com este nome.");
                    return View(Hierarquia);
                }

                // Verificar se o nível de hierarquia já foi atribuído a outra hierarquia
                if (_context.Hierarquias.Any(c => c.NivelHierarquico == Hierarquia.NivelHierarquico))
                {
                    ModelState.AddModelError("NivelHierarquia", "Este nível de hierarquia já foi atribuído a outra hierarquia.");
                    return View(Hierarquia);
                }

                _context.Add(Hierarquia);
                await _context.SaveChangesAsync();

                TempData["MensagemCriadoSuccess"] = "Hierarquia criado com sucesso!";

                return RedirectToAction(nameof(Details), new { id = Hierarquia.HierarquiaId });
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
            if (id != Hierarquia.HierarquiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Verificar se o nome do Hierarquia já existe (exceto para o Hierarquia sendo editado)
                if (_context.Hierarquias.Any(c => c.HierarquiaNome == Hierarquia.HierarquiaNome && c.HierarquiaId != Hierarquia.HierarquiaId))
                {
                    ModelState.AddModelError("Nome", "Já existe um Hierarquia com este nome.");
                    return View(Hierarquia);
                }
                if (_context.Hierarquias.Any(c => c.NivelHierarquico == Hierarquia.NivelHierarquico))
                {
                    ModelState.AddModelError("NivelHierarquia", "Este nível de hierarquia já foi atribuído a outra hierarquia.");
                    return View(Hierarquia);
                }

                try
                {
                    _context.Update(Hierarquia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HierarquiaExists(Hierarquia.HierarquiaId))
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
                .FirstOrDefaultAsync(m => m.HierarquiaId == id);
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

            return RedirectToAction(nameof(AllHierarquias));
        }

        public IActionResult ItemNaoEncontrado()
        {
            return View();
        }

        // GET: Hierarquias/AssignHierarquia/5
        public IActionResult AssignHierarquia(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Obter o empregado pelo ID
            var employee = _context.Employee.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            // Lógica para atribuir hierarquia ao empregado com o ID especificado
            // Exemplo: Atribuir hierarquia de nível 1
            employee.Hierarquia = new HierarquiasModel
            {
                HierarquiaNome = "Chefe",
                NivelHierarquico = 1
            };

            // Salvar as alterações no contexto
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Hierarquias/AssignFuncao/5
        public IActionResult AssignFuncao(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Obter o empregado pelo ID
            var employee = _context.Employee.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            // Lógica para atribuir função ao empregado com o ID especificado
            // Exemplo: Atribuir a função "Funcionario Padrão"
            employee.Funcao = new Funcao
            {
                NomeFuncao = "Funcionario Padrão",
                DescricaoFuncao = "Descrição da função padrão"
            };

            // Salvar as alterações no contexto
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    private bool HierarquiaExists(int id)
        {
            return _context.Hierarquias.Any(e => e.HierarquiaId == id);
        }
    }
}