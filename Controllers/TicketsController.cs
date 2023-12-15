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
    public class TicketsController : Controller
    {
        private readonly SupermarketDbContext _context;
        // Propriedade calculada para obter a duração média de atendimento
        public TimeSpan? DuracaoAtendimento
        {
            get
            {
                // Obtém a primeira Data de Emissão (DataEmicao) do Ticket
                var de = _context.Tickets.Select(t => new { t.DataEmicao }).FirstOrDefault();

                // Obtém a primeira Data de Atendimento (DataAtendimento) do Ticket
                var da = _context.Tickets.Select(t => new { t.DataAtendimento }).FirstOrDefault();

                // Verifica se as datas de Emissão e Atendimento foram obtidas com sucesso
                if (de != null && da != null)
                {
                    // Extrai os valores das propriedades DataEmicao e DataAtendimento
                    var deP = de.DataEmicao;
                    var daP = da.DataAtendimento;

                    // Obtém a quantidade média de departamentos da tabela Departments
                    var MediaDep = _context.Departments.Select(t => new { t.QuatDepMed }).FirstOrDefault();

                    // Verifica se a quantidade média de departamentos foi obtida com sucesso
                    if (MediaDep != null)
                    {
                        // Obtém o valor da quantidade média de departamentos
                        int QuatDepMed = MediaDep.QuatDepMed;

                        // Inicializa um TimeSpan para calcular a média
                        TimeSpan media = TimeSpan.Zero;

                        // Loop para calcular a diferença entre as datas e somar ao total
                        for (int i = 1; i <= QuatDepMed; i++)
                        {
                            var aux = (daP - deP);
                            media += aux;
                        }

                        // Calcula a média e retorna como TimeSpan
                        if (QuatDepMed > 0)
                        {
                            media = TimeSpan.FromTicks(media.Ticks / QuatDepMed);
                            return media;
                        }
                        else
                        {
                            // Retorna null se a quantidade média de departamentos for 0
                            return null;
                        }
                    }
                    else
                    {
                        // Retorna null caso não haja registros na tabela Departments
                        return null;
                    }
                }
                else
                {
                    // Retorna null se as datas de Emissão e Atendimento não foram obtidas
                    return null;
                }
            }
        }
        public TicketsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
              return _context.Tickets != null ? 
                          View(await _context.Tickets.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.Tickets'  is null.");
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketId,DataEmicao,DataAtendimento,NumeroDaSenha,Estado,Priorioritario,IDDepartments")] Tickets tickets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tickets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tickets);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets.FindAsync(id);
            if (tickets == null)
            {
                return NotFound();
            }
            return View(tickets);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,DataEmicao,DataAtendimento,NumeroDaSenha,Estado,Priorioritario,IDDepartments")] Tickets tickets)
        {
            if (id != tickets.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tickets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketsExists(tickets.TicketId))
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
            return View(tickets);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Tickets'  is null.");
            }
            var tickets = await _context.Tickets.FindAsync(id);
            if (tickets != null)
            {
                _context.Tickets.Remove(tickets);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketsExists(int id)
        {
          return (_context.Tickets?.Any(e => e.TicketId == id)).GetValueOrDefault();
        }
    }
}
