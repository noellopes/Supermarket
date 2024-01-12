using System;
using System.Collections.Generic;
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
    public class FuncaoController : Controller
    {
        private readonly SupermarketDbContext _context;

        public FuncaoController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Funcao
        public async Task<IActionResult> Index(int page = 1, string descricao ="", string funcao="" )
        {
            var filtros = _context.Funcao.AsQueryable();
           

            if (descricao != "")
            {
                filtros = filtros.Where(f => f.DescricaoFuncao.Contains(descricao));
            }
            if (funcao != "")
            {
                filtros = filtros.Where(f => f.NomeFuncao.Contains(funcao));
            }
            

            ViewBag.FiltroDescricao = descricao;
            ViewBag.FiltroFuncao = funcao;
            var pagination = new PagingInfo
            {
                CurrentPage = page,
                PageSize = PagingInfo.DEFAULT_PAGE_SIZE,
                TotalItems = filtros.Count()
            };
            return View(
                new FuncaoListViewModel {
                    funcao = filtros.OrderBy( f => f.NomeFuncao)
                        .Skip((page-1)*pagination.PageSize).Take(pagination.PageSize),
                    Pagination = pagination 
                }
            );

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
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([Bind("FuncaoId,NomeFuncao,DescricaoFuncao")] Funcao funcao)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    bool funcaoExiste = await _context.Funcao.AnyAsync(
                        f => f.NomeFuncao == funcao.NomeFuncao || f.FuncaoId == funcao.FuncaoId);
                    if (!funcaoExiste)
                    {
                        _context.Add(funcao);
                        await _context.SaveChangesAsync();

                        ViewBag.Mensagem = "Funcao Criada com sucesso";
                        
                        return View("Details", funcao);
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
                _context.Add(funcao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funcao);
        }

        // GET: Funcao/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Funcao == null)
            {
                return NotFound();
            }

            var Funcao = await _context.Funcao.FindAsync(id);
            if (Funcao == null)
            {
                return NotFound();
            }
            return View(Funcao);
        }

        // POST: Funcao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("FuncaoId,NomeFuncao,DescricaoFuncao")] Funcao funcao)
        {
            if (id != funcao.FuncaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool funcaoExiste = await _context.Funcao.AnyAsync(
                        f => f.NomeFuncao == funcao.NomeFuncao || f.FuncaoId == funcao.FuncaoId);

                    bool funcaoIgual = await _context.Funcao.AnyAsync(
                        f => (f.NomeFuncao == funcao.NomeFuncao || f.FuncaoId == funcao.FuncaoId) && f.DescricaoFuncao == funcao.DescricaoFuncao);
                    if (funcaoExiste)
                    {
                        if (!funcaoIgual)
                        {
                            _context.Update(funcao);
                            /*bool funcaoExiste = await _context.Funcao.AnyAsync(
                            f => f.NomeFuncao == Funcao.NomeFuncao || f.FuncaoId == Funcao.FuncaoId);

                            bool funcaoIgual = await _context.Funcao.AnyAsync(
                            f => (f.NomeFuncao == Funcao.NomeFuncao || f.FuncaoId == Funcao.FuncaoId) && f.DescricaoFuncao == Funcao.DescricaoFuncao);*/
                        }
                        if (funcaoExiste)
                        {
                            if (!funcaoIgual)
                            {
                                _context.Update(funcao);
                                await _context.SaveChangesAsync();
                                TempData["MensagemPositiva"] = "Edicao de uma funcao ja existente com sucesso";
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                TempData["Mensagem"] = "funcao existente";
                                return RedirectToAction(nameof(Index));
                            }
                        }
                        else
                        {
                            _context.Update(funcao);
                            await _context.SaveChangesAsync();
                            TempData["MensagemPositiva"] = "Edicao realizada com sucesso";
                            return View("Details", funcao);
                            _context.Update(funcao);
                            await _context.SaveChangesAsync();
                            TempData["MensagemPositiva"] = "Edicao realizada com sucesso";
                            return View("Details", funcao);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncaoExists(funcao.FuncaoId))
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
            return View(funcao);
        }

        // GET: Funcao/Delete/5
        [Authorize(Roles = "Manager")]
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
        [Authorize(Roles = "Manager")]
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
