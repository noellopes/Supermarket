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
    public class FuncaoController : Controller
    {
        private readonly SupermarketDbContext _context;

        public FuncaoController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Funcao
        public async Task<IActionResult> Index()
        {
              return _context.Funcao != null ? 
                          View(await _context.Funcao.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.Funcao'  is null.");
        }

        // GET: Funcao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Funcao == null)
            {
                return NotFound();
            }

            var Funcao = await _context.Funcao
                .FirstOrDefaultAsync(m => m.FuncaoId == id);
            if (Funcao == null)
            {
                return NotFound();
            }

            return View(Funcao);
        }

        // GET: Funcao/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FuncaoId,NomeFuncao,DescricaoFuncao")] Funcao Funcao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool funcaoExiste = await _context.Funcoes.AnyAsync(
                        f => f.NomeFuncao == funcoes.NomeFuncao || f.IdFuncao == funcoes.IdFuncao);
                    if (!funcaoExiste)
                    {
                        _context.Add(funcoes);
                        await _context.SaveChangesAsync();

                        ViewBag.Mensagem = "Funcao Criada com sucesso";
                        
                        return View("Details", funcoes);
                    }
                    else //funcao existe
                    {
                        TempData["Mensagem"] = "Este Funcao ja existe";
                        //ModelState.AddModelError("", "Este Funcao ja existe");
                    }
                    
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    //return ex;
                }
                _context.Add(Funcao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Funcao);
        }

        // GET: Funcao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Funcao == null)
            {
                return NotFound();
            }

            var Funcao = await _context.Funcao.FindAsync(id);
            if (Funcao == null)
            {
                TempData["mensagem"] = "A funcao nao existe";
                return RedirectToAction(nameof(Index));
            }
            return View(Funcao);
        }

        // POST: Funcao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FuncaoId,NomeFuncao,DescricaoFuncao")] Funcao Funcao)
        {
            if (id != Funcao.FuncaoId)
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
                    bool funcaoExiste = await _context.Funcao.AnyAsync(
                        f => f.NomeFuncao == Funcao.NomeFuncao || f.FuncaoId == Funcao.FuncaoId);

                    bool funcaoIgual = await _context.Funcao.AnyAsync(
                        f => (f.NomeFuncao == Funcao.NomeFuncao || f.FuncaoId == Funcao.FuncaoId) && f.DescricaoFuncao == Funcao.DescricaoFuncao);
                    if (funcaoExiste)
                    {
                        if (!funcaoIgual) {
                            _context.Update(Funcao);
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
                        _context.Update(Funcao);
                        await _context.SaveChangesAsync();
                        TempData["MensagemPositiva"] = "Edicao realizada com sucesso";
                        return View("Details", Funcao);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncaoExists(Funcao.FuncaoId))
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
            return View(Funcao);
        }

        // GET: Funcao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Funcao == null)
            {
                return NotFound();
            }

            var Funcao = await _context.Funcao
                .FirstOrDefaultAsync(m => m.FuncaoId == id);
            if (Funcao == null)
            {
                TempData["MensagemPositiva"] = "A funcao foi deletada com sucesso";
                return RedirectToAction(nameof(Index));
            }

            return View(Funcao);
        }

        // POST: Funcao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Funcao == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Funcao'  is null.");
            }
            var Funcao = await _context.Funcao.FindAsync(id);
            if (Funcao != null)
            {
                TempData["MensagemPositiva"] = "A funcao foi deletada com sucesso";
                _context.Funcoes.Remove(funcoes);
                _context.Funcao.Remove(Funcao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncaoExists(int id)
        {
          return (_context.Funcao?.Any(e => e.FuncaoId == id)).GetValueOrDefault();
        }
    }
}
