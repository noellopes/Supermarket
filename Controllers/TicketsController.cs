using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        public TicketsController(SupermarketDbContext context)
        {
            _context = context;
        }



        // GET: Tickets
        [Authorize(Roles = "Gestor,Funcionário,Cliente")]
        public async Task<IActionResult> Index(int page = 1, int departmentName = 0)
        {


            ViewData["IDDepartments"] = new SelectList(_context.Set<Department>(), "IDDepartments", "NameDepartments");

            var tickets = from b in _context.Tickets.Include(b => b.Departments) select b;


            if (departmentName != 0)
            {
                tickets = tickets.Where(x => x.IDDepartments == departmentName);
            }

            var Departments = await _context.Departments.ToListAsync();


            PagingInfo paging = new PagingInfo
            {
                CurrentPage = page,
                TotalItems = await tickets.CountAsync(),
            };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }

            var tm = new TicketViewModel
            {
                Tickets = await tickets
                .OrderBy(b => b.TicketId)
                .Skip((paging.CurrentPage - 1) * paging.PageSize)
                   .Take(paging.PageSize)
                   .ToListAsync(),
                Departments = Departments,
                PagingInfo = paging,
                SearchDepartment = departmentName,
                //SearchButtonDepartment = departmentButtonName,
            };

           return View(tm);

        }

        private int GetDepartmentId(string departmentName)
        {
            return _context.Departments
                .Where(a => a.NameDepartments == departmentName)
                .Select(a => a.IDDepartments)
                .FirstOrDefault();
        }

        // GET: Tickets/Details/5
        [Authorize(Roles = "Gestor,Funcionário,Cliente")]
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
        //public IActionResult Create()
        //{

        //    ViewData["IDDepartments"] = new SelectList(_context.Set<Department>(), "IDDepartments", "NameDepartments");

        //    return View("Index");
        //}

        //// POST: Tickets/Create

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("TicketId,DataEmissao,DataAtendimento,NumeroDaSenha,Estado,Prioritario,IDDepartments")] Ticket tickets)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        // Set default or automated values for the new ticket
        //        tickets.DataEmissao = DateTime.Now;
        //        tickets.Estado = false; // Set default value
        //        tickets.Prioritario = false; // Set default value

        //        // Add the new ticket to the context
        //        _context.Add(tickets);

        //        // Save changes to the database
        //        await _context.SaveChangesAsync();

        //        ViewBag.Message = "Ticket created successfully.";
        //        return View("Details", tickets);
        //    }

        //    return View(tickets);
        //}

        //if (ModelState.IsValid)
        //{
        //    _context.Add(tickets);
        //    await _context.SaveChangesAsync();
        //    ViewBag.Message = "Ticket created sucessfully.";
        //    return View("Details",tickets);
        //}
        //return View(tickets);
        [Authorize(Roles = "Gestor,Funcionário,Cliente")]
        public IActionResult Create()
        {
            //// Set default values for the ticket
            //var newTicket = new Ticket
            //{
            //    DataEmissao = DateTime.Now,
            //    DataAtendimento = null,
            //    NumeroDaSenha = 0,
            //    Estado = false,
            //    Prioritario = false,
            //    IDDepartments = 1
            //    // Set other properties with default values
            //};

            return View();
        }
        [Authorize(Roles = "Gestor,Funcionário,Cliente")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketId,DataEmissao,DataAtendimento,NumeroDaSenha,Estado,Prioritario,IDDepartments")] Ticket ticket, int departmentId =0)
        {
           
            var ticketlista = await _context.Tickets.ToListAsync();

            var schedules = await _context.Schedule.Where(b => b.IDDepartments == departmentId).FirstOrDefaultAsync();

            // Perform validation and save the new ticket to the database

            //  if (ModelState.IsValid && (DateTime.Now.Date<= schedules.EndDate.Value.Date&& DateTime.Now.Date >= schedules.StartDate.Date) && (DateTime.Now.Hour <= schedules.DailyFinishTime.Hour && DateTime.Now.Hour >= schedules.DailyStartTime.Hour && DateTime.Now.Minute <= schedules.DailyFinishTime.Minute && DateTime.Now.Minute >= schedules.DailyStartTime.Minute))
            if (ModelState.IsValid && DateTime.Now >= schedules.StartDate.Date && DateTime.Now <= schedules.EndDate.Value.Date)
            {
                // Check if the current time is within the daily time range
                TimeSpan currentTimeOfDay = DateTime.Now.TimeOfDay;

                if (currentTimeOfDay >= schedules.DailyStartTime.TimeOfDay && currentTimeOfDay <= schedules.DailyFinishTime.TimeOfDay) { 
                    ticket.DataEmissao = DateTime.Now;
                ticket.DataAtendimento = null;
                ticket.NumeroDaSenha = ticketlista.Last().NumeroDaSenha + 1;
                ticket.Estado = false;
                ticket.Prioritario = false;
                ticket.IDDepartments = departmentId;

                // Save the new ticket to the database
                _context.Tickets.Add(ticket);
                _context.SaveChanges();

                return RedirectToAction("Index"); // Redirect to the ticket list or another action
                }
            }

            return View(ticket);
        }

        // GET: TicketsPriority/Create
        [Authorize(Roles = "Gestor,Funcionário,Cliente")]
        public IActionResult CreatePriority()
        {

            return View();
        }

        // POST: TicketsPriority/Create
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Funcionário,Cliente")]
        public async Task<IActionResult> CreatePriority([Bind("TicketId,DataEmissao,DataAtendimento,NumeroDaSenha,Estado,Prioritario,IDDepartments")] Ticket ticket, int departmentId = 0)
        {

            var ticketlista = await _context.Tickets.ToListAsync();


            var schedules = await _context.Schedule.Where(b => b.IDDepartments == departmentId).FirstOrDefaultAsync();

            // Perform validation and save the new ticket to the database

            //  if (ModelState.IsValid && (DateTime.Now.Date<= schedules.EndDate.Value.Date&& DateTime.Now.Date >= schedules.StartDate.Date) && (DateTime.Now.Hour <= schedules.DailyFinishTime.Hour && DateTime.Now.Hour >= schedules.DailyStartTime.Hour && DateTime.Now.Minute <= schedules.DailyFinishTime.Minute && DateTime.Now.Minute >= schedules.DailyStartTime.Minute))
            if (ModelState.IsValid && DateTime.Now >= schedules.StartDate.Date && DateTime.Now <= schedules.EndDate.Value.Date)
            {

                ticket.DataEmissao = DateTime.Now;
                ticket.DataAtendimento = null;
                ticket.NumeroDaSenha = ticketlista.Last().NumeroDaSenha + 1;
                ticket.Estado = false;
                ticket.Prioritario = true;
                ticket.IDDepartments = departmentId;

                // Save the new ticket to the database
                _context.Tickets.Add(ticket);
                _context.SaveChanges();

                return RedirectToAction("Index"); // Redirect to the ticket list or another action
            }

            return View(ticket);

        }
        //if (ModelState.IsValid)
        //{
        //    _context.Add(tickets);
        //    await _context.SaveChangesAsync();
        //    ViewBag.Message = "Ticket created sucessfully.";
        //    return View("Details",tickets);
        //}
        //return View(tickets);
        [Authorize(Roles = "Gestor,Funcionário,Cliente")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["IDDepartments"] = new SelectList(_context.Set<Department>(), "IDDepartments", "NameDepartments");

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Funcionário,Cliente")]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,DataEmissao,DataAtendimento,NumeroDaSenha,Estado,Prioritario,IDDepartments")] Ticket tickets)
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

        [Authorize(Roles = "Gestor,Funcionário")]
        public IActionResult Atender(int id)
        {
            // Retrieve the ticket with the given ID from the database
            var ticket = _context.Tickets.Find(id);

            if (ticket == null)
            {
                return NotFound(); // Handle the case where the ticket with the given ID is not found
            }

            // Call a method to perform the "Atender" operation
            ticket.DataAtendimento = DateTime.Now;
            ticket.Estado = true;

            _context.Update(ticket);
            // Save changes to the database
            _context.SaveChanges();

            // Redirect to the Index or another action
            return RedirectToAction("Index");
        }

        //// GET: Tickets/Edit/5
        //[Authorize(Roles = "Gestor,Funcionário,Cliente")]
        //public async Task<IActionResult> Atender(int? id)
        //{
        //    ViewData["IDDepartments"] = new SelectList(_context.Set<Department>(), "IDDepartments", "NameDepartments");

        //    if (id == null || _context.Tickets == null)
        //    {
        //        return NotFound();
        //    }

        //    var tickets = await _context.Tickets.FindAsync(id);
        //    if (tickets == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(tickets);
        //}

        //// POST: Tickets/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Gestor,Funcionário,Cliente")]
        //public async Task<IActionResult> Atender(int id, [Bind("TicketId,DataEmissao,DataAtendimento,NumeroDaSenha,Estado,Prioritario,IDDepartments")] Ticket tickets)
        //{
        //    if (id != tickets.TicketId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            tickets.Estado = true;
        //            tickets.DataAtendimento = DateTime.Now;
        //            _context.Update(tickets);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction("Index");
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TicketsExists(tickets.TicketId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //    }
        //    return View(tickets);
        //}

        // GET: Tickets/Delete/5
        [Authorize(Roles = "Gestor,Funcionário,Cliente")]
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
        [Authorize(Roles = "Gestor,Funcionário,Cliente")]
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
