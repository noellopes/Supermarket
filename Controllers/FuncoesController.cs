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
    public class FuncoesController : Controller
    {
        private readonly SupermarketDbContext _context;

        public FuncoesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Funcoes
        public async Task<IActionResult> Index()
        {
              return _context.Funcoes != null ? 
                          View(await _context.Funcoes.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.Funcoes'  is null.");
        }

        // GET: Funcoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Funcoes == null)
            {
                return NotFound();
            }

            var funcoes = await _context.Funcoes
                .FirstOrDefaultAsync(m => m.IdFuncao == id);
            if (funcoes == null)
            {
                return NotFound();
            }

            return View(funcoes);
        }

        // GET: Funcoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFuncao,NomeFuncao,DescricaoFuncao")] Funcoes funcoes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funcoes);
        }

        // GET: Funcoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Funcoes == null)
            {
                return NotFound();
            }

            var funcoes = await _context.Funcoes.FindAsync(id);
            if (funcoes == null)
            {
                TempData["mensagem"] = "A funcao nao existe";
                return RedirectToAction(nameof(Index));
            }
            return View(funcoes);
        }

        // POST: Funcoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFuncao,NomeFuncao,DescricaoFuncao")] Funcoes funcoes)
        {
            if (id != funcoes.IdFuncao)
            {
                return NotFound();
            }

            if (ModelState.IsValid) //valida no lado do cliente e o do servidosr [neste caso no cliente]
            {
                try
                {
                    bool funcaoExiste = await _context.Funcoes.AnyAsync(
                        f => f.NomeFuncao == funcoes.NomeFuncao || f.IdFuncao == funcoes.IdFuncao);

                    bool funcaoIgual = await _context.Funcoes.AnyAsync(
                        f => (f.NomeFuncao == funcoes.NomeFuncao || f.IdFuncao == funcoes.IdFuncao) && f.DescricaoFuncao == funcoes.DescricaoFuncao);
                    if (funcaoExiste)
                    {
                        if (!funcaoIgual) {
                            _context.Update(funcoes);
                            await _context.SaveChangesAsync();
                            TempData["MensagemPositiva"] = "Edicao de uma funcao ja existente com sucesso";
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            TempData["Mensagem"] = "funcao identica";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    else{
                        _context.Update(funcoes);
                        await _context.SaveChangesAsync();
                        TempData["MensagemPositiva"] = "Edicao realizada com sucesso";
                        return View("Details", funcoes);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncoesExists(funcoes.IdFuncao))
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
            return View(funcoes);
        }

        // GET: Funcoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Funcoes == null)
            {
                return NotFound();
            }

            var funcoes = await _context.Funcoes
                .FirstOrDefaultAsync(m => m.IdFuncao == id);
            if (funcoes == null)
            {
                return NotFound();
            }

            return View(funcoes);
        }

        // POST: Funcoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Funcoes == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Funcoes'  is null.");
            }
            var funcoes = await _context.Funcoes.FindAsync(id);
            if (funcoes != null)
            {
                _context.Funcoes.Remove(funcoes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncoesExists(int id)
        {
          return (_context.Funcoes?.Any(e => e.IdFuncao == id)).GetValueOrDefault();
        }
    }
}
