<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
=======
﻿using Microsoft.AspNetCore.Mvc;
>>>>>>> FolgasPendentesAprovadas
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class PontosController : Controller
    {
        private readonly SupermarketDbContext _context;

        public PontosController(SupermarketDbContext context)
        { 
            _context = context;
        }

        // GET: Pontoes
        public async Task<IActionResult> Index(int page = 1, DateTime? searchMonth = null)
        {
            // Obtém todos os pontos sem modificar a lista completa
            var allPoints = await _context.Ponto.Include(p => p.Employee).ToListAsync();

            var date = _context.Ponto.AsQueryable();  // Certifique-se de que 'date' seja IQueryable<Ponto>

            if (searchMonth.HasValue)
            {
                date = date
                    .Where(x => x.Date.HasValue &&
                                x.Date.Value.Year == searchMonth.Value.Year &&
                                x.Date.Value.Month == searchMonth.Value.Month);
            }

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

            var vm = new PontoDateViewModel
            {
                Pontos = await date
                    .OrderBy(x => x.Date)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),  // Agora, 'ToListAsync' deve funcionar corretamente
                PagingInfo = paging,
                SearchMonth = searchMonth,
            };

            // Calcular as horas extras para cada Ponto no ViewModel
            foreach (var ponto in vm.Pontos)
            {
                CalculateExtraHours(ponto);
            }

            return View(vm);
        }

        private void CalculateExtraHours(Ponto ponto)
        {
            if (!string.IsNullOrEmpty(ponto.CheckOutTime) && !string.IsNullOrEmpty(ponto.RealCheckOutTime))
            {
                TimeSpan outTime = TimeSpan.Parse(ponto.CheckOutTime);
                TimeSpan realoutTime = TimeSpan.Parse(ponto.RealCheckOutTime);

                if (realoutTime > outTime)
                {
                    TimeSpan extraHours = realoutTime - outTime;
                    ponto.ExtraHours = extraHours;
                }
                else
                {
                    TimeSpan extraHours = outTime - realoutTime;
                    ponto.ExtraHours = extraHours;
                }
            }
            else
            {
                ponto.ExtraHours = TimeSpan.Zero;
            }
        }

        // GET: Pontoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ponto == null)
            {
                return NotFound();
            }

            var ponto = await _context.Ponto
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.PontoId == id);
            if (ponto == null)
            {
                return NotFound();
            }

            return View(ponto);
        }

        // GET: Pontoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async void Create()
        {
            var funcionario = await _context.Employee.Where(x => x.Employee_Email == User.Identity.Name).FirstOrDefaultAsync();
            if (funcionario is not null)
            {
                var escala = await _context.EmployeeSchedule.Where(x => x.EmployeeId == funcionario.EmployeeId && x.Date==DateTime.Now.Date).FirstOrDefaultAsync();
                var pontoDia = await _context.Ponto.Where(x => x.EmployeeId == funcionario.EmployeeId && x.Date == DateTime.Now.Date).FirstOrDefaultAsync();
                if (pontoDia is not null)
                {
                    if(pontoDia.CheckInTime is not null && pontoDia.LunchStartTime is null && pontoDia.LunchEndTime is null && pontoDia.CheckOutTime is null)
                    {
                        pontoDia.LunchStartTime = DateTime.Now.ToString("HH:mm");
                    }
                    else if(pontoDia.CheckInTime is not null && pontoDia.LunchStartTime is not null && pontoDia.LunchEndTime is null && pontoDia.CheckOutTime is null)
                    {
                        pontoDia.LunchEndTime = DateTime.Now.ToString("HH:mm");
                    }
                    else if (pontoDia.CheckInTime is not null && pontoDia.LunchStartTime is not null && pontoDia.LunchEndTime is not null && pontoDia.CheckOutTime is null)
                    {
                        pontoDia.CheckOutTime = DateTime.Now.ToString("HH:mm");
                    }
                    else
                    {

                    }
                    _context.Update(pontoDia);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var ponto = new Ponto
                    {
                        EmployeeId = funcionario.EmployeeId,
                        Date = DateTime.Now.Date,
                        CheckInTime = DateTime.Now.ToString("HH:mm")
                    };

                    _context.Add(ponto);
                    await _context.SaveChangesAsync();
                }
            }
        }

        // GET: Pontoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ponto == null)
            {
                return NotFound();
            }

            var ponto = await _context.Ponto.FindAsync(id);
            if (ponto == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", ponto.EmployeeId);
            return View(ponto);
        }

        // POST: Pontoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PontoId,EmployeeId,Date,CheckInTime,CheckOutTime,LunchStartTime,LunchEndTime,RealCheckOutTime,Status,Justificative,ExtraHours")] Ponto ponto)
        {
            if (id != ponto.PontoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ponto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PontoExists(ponto.PontoId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", ponto.EmployeeId);
            return View(ponto);
        }

        // GET: Pontoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ponto == null)
            {
                return NotFound();
            }

            var ponto = await _context.Ponto
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.PontoId == id);
            if (ponto == null)
            {
                return NotFound();
            }

            return View(ponto);
        }

        // POST: Pontoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ponto == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Ponto'  is null.");
            }
            var ponto = await _context.Ponto.FindAsync(id);
            if (ponto != null)
            {
                _context.Ponto.Remove(ponto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PontoExists(int id)
        {
            return (_context.Ponto?.Any(e => e.PontoId == id)).GetValueOrDefault();
        }
    }
}