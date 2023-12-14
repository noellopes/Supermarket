﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class FolgasController : Controller
    {
        private readonly SupermarketDbContext _context;

        public FolgasController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Folgas
        public async Task<IActionResult> Index()
        {
              return _context.Folga != null ? 
                          View(await _context.Folga.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.Folga'  is null.");
        }

        // GET: Folgas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Folga == null)
            {
                return NotFound();
            }

            var folga = await _context.Folga
                .FirstOrDefaultAsync(m => m.FolgaId == id);
            if (folga == null)
            {
                return NotFound();
            }

            return View(folga);
        }

        // GET: Folgas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Folgas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FolgaId,Gestor,Status,DataPedido,DataInicio,DataFim,Motivo")] Folga folga)
        {
            if (ModelState.IsValid)
            {
                folga.DataPedido = DateTime.Now;
                folga.Status = "Pendente";
                _context.Add(folga);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "A sua folga foi adicionada com sucesso. Brevemente iremos dar resposta ao seu pedido";
                return RedirectToAction(nameof(Index));
            }
            return View(folga);
        }

        // GET: Folgas/Edit/5
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
            return View(folga);
        }

        // POST: Folgas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FolgaId,Gestor,Status,DataPedido,DataInicio,DataFim,Motivo")] Folga folga)
        {
            if (id != folga.FolgaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    folga.DataPedido = DateTime.Now;
                    folga.Status = "Pendente";
                    _context.Update(folga);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "A sua folga foi adicionada com sucesso. Brevemente iremos dar resposta ao seu pedido";
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
            return View(folga);
        }

        // GET: Folgas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Folga == null)
            {
                return NotFound();
            }

            var folga = await _context.Folga
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
            TempData["SuccessMessage"] = "A sua folga foi eliminada com sucesso.";

            return RedirectToAction(nameof(Index));
        }

        private bool FolgaExists(int id)
        {
          return (_context.Folga?.Any(e => e.FolgaId == id)).GetValueOrDefault();
        }

        public IActionResult FolgasPendentes()
        {
            var FolgasPendentes = _context.Folga.Where(f => f.Status == "Pendente").ToList();
            return View(FolgasPendentes);
        }
        //Método para aprovar ou rejeitar folga
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult>AprovarRejeitarFolga(int folgaId, bool aprovar, string gestor)
        {
            var folga = await _context.Folga.FindAsync(folgaId);

            if (folga == null)
            {
                return NotFound();
            }
            folga.Gestor = gestor;
            folga.Status = aprovar ? "Aprovada" : "Rejeitada";

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(FolgasPendentes));
        }

        public IActionResult FolgasAprovadas()
        {
            var FolgasAprovadas = _context.Folga.Where(f => f.Status == "Aprovada").ToList();
            return View(FolgasAprovadas);
        }

    }
}
