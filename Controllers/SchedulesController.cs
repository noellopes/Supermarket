using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Supermarket.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly SupermarketDbContext _context;

        public SchedulesController(SupermarketDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Index(int page = 1, int departmentDrop = 0, bool BotaoHistorico = false)
        {
            ViewData["IDDepartments"] = new SelectList(_context.Set<Department>(), "IDDepartments", "NameDepartments");

            var schedules = from b in _context.Schedule.Include(b => b.Departments)
                            select b;

            if (BotaoHistorico == false)
            {
       
                schedules = schedules.Where(b => b.StartDate.Date <= DateTime.Now.Date && b.EndDate.Value.Date >= DateTime.Now.Date);

                if (departmentDrop != 0)
                {
                    schedules = schedules.Where(x => x.IDDepartments == departmentDrop);
                }

                
            }

            PagingInfo paging = new PagingInfo
            {
                CurrentPage = page,
                PageSize = 4,
                TotalItems = await schedules.CountAsync(),
            };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }

            var Departments = await _context.Departments.ToListAsync();

            var vm = new SchedulesViewModel
            {
                Schedules = await schedules
                    .OrderBy(b => b.ScheduleId)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),
                Departments = Departments,
                PagingInfo = paging,
                SearchDepartment = departmentDrop,
            };

            return View(vm);
        }


        // GET: Schedules/Details/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Schedule == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
           
            if (schedule == null)
            {
                return NotFound();
            }
            // Fetch the department name based on IDDepartments
            var departmentName = _context.Departments
                .Where(d => d.IDDepartments == schedule.IDDepartments)
                .Select(d => d.NameDepartments)
                .FirstOrDefault();

            // Pass the departmentName to the view
            ViewData["DepartmentName"] = departmentName;



            return View(schedule);
        }

        // GET: Schedules/Create
        [Authorize(Roles = "Gestor")]
        public IActionResult Create()
        {
            ViewData["IDDepartments"] = new SelectList(_context.Set<Department>(), "IDDepartments", "NameDepartments");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create([Bind("ScheduleId,StartDate,EndDate,DailyStartTime,DailyFinishTime,IDDepartments")] Schedule schedule)
        {

            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }



            return View(schedule);
        }

        // GET: Schedules/Edit/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int? id)
        {

            ViewData["IDDepartments"] = new SelectList(_context.Set<Department>(), "IDDepartments", "NameDepartments");

            if (id == null || _context.Schedule == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,StartDate,EndDate,DailyStartTime,DailyFinishTime,IDDepartments")] Schedule schedule)
        {
            if (id != schedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.ScheduleId))
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
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Schedule == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Schedule == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Schedule'  is null.");
            }
            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedule.Remove(schedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return (_context.Schedule?.Any(e => e.ScheduleId == id)).GetValueOrDefault();
        }

        [Authorize(Roles = "Gestor")]

        public async Task<IActionResult> Afluencias(int procuraLimiteAfluencia = 0, DateTime? procuraDataInicial = null, DateTime? procuraDataFinal = null)
        {
            var model = new List<AfluenciasViewModel>();



            var departmentsWithAfluencia = _context.Departments
                .Select(d => new
                {
                    Department = d,
                    AfluenciaCount = _context.Tickets
                        .Count(t => t.IDDepartments == d.IDDepartments &&
                                    t.DataAtendimento >= procuraDataInicial &&
                                    t.DataAtendimento <= procuraDataFinal)
                })
                .Where(result => result.AfluenciaCount >= procuraLimiteAfluencia)
                .ToList();

            foreach (var result in departmentsWithAfluencia)
            {
                var afluencias = _context.Tickets
                    .Where(t => t.IDDepartments == result.Department.IDDepartments &&
                                t.DataAtendimento >= procuraDataInicial &&
                                t.DataAtendimento <= procuraDataFinal)
                    .OrderBy(t => t.DataAtendimento)
                    .ToList();

                // Check for spikes within the specified time interval
                var spikeAfluencias = FindTicketSpikes(afluencias);

                var am = new AfluenciasViewModel
                {
                    Department = result.Department,
                    Tickets = spikeAfluencias, // No need to query the database again
                    SearchDataIntervaloInicial = procuraDataInicial,
                    SearchDataIntervaloFinal = procuraDataFinal
                };

                //comando para debug
                Console.WriteLine($"Model count: {model.Count}, procuraLimiteAfluencia: {procuraLimiteAfluencia}, procuraDataInicial: {procuraDataInicial}, procuraDataFinal: {procuraDataFinal}");

                model.Add(am);
            }

            Console.WriteLine($"procuraDataInicial: {procuraDataInicial}, procuraDataFinal: {procuraDataFinal}");

            // Check if any filters are present
            if (procuraDataInicial != null || procuraDataFinal != null || procuraLimiteAfluencia > 0)
            {
                return View("Afluencias", model);
            }
            else
            {
                ViewData["NoDataMessage"] = "No data displayed. Please provide the filters to detect the spikes.";
                return View();
            }
        }

        // Helper method to find spikes within a 10-minute interval
        private List<Ticket> FindTicketSpikes(List<Ticket> tickets)
        {
            List<Ticket> spikeAfluencias = new List<Ticket>();

            for (int i = 0; i < tickets.Count - 1; i++)
            {
                //Grab the interval bnetween tickts print
                TimeSpan? interval = tickets[i + 1].DataAtendimento - tickets[i].DataAtendimento;

                if (interval?.TotalMinutes <= 10)
                {
                    spikeAfluencias.Add(tickets[i]);
                }
            }

            // Include the last ticket if it's part of a spike
            if (tickets.Count > 0)
            {
                spikeAfluencias.Add(tickets.Last());
            }

            return spikeAfluencias;
        }

    }
    }

