using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Index(int page = 1, int departmentDrop = 0 /*int departmentButtonName = 0*/)
        {
            ViewData["IDDepartments"] = new SelectList(_context.Set<Department>(), "IDDepartments", "NameDepartments");

            var schedules = from b in _context.Schedule.Include(b => b.Departments) select b;
            //var schedules = _context.Schedule.Include(s => s.Departments).ToList();


            if (departmentDrop!=0)
            {
                schedules = schedules.Where(x => x.IDDepartments==departmentDrop);
            }

            //if (departmentButtonName != 0)
            //{
            //    schedules = schedules.Where(x => x.IDDepartments.Equals(departmentButtonName));
            //}

            PagingInfo paging = new PagingInfo
            {
                CurrentPage = page,
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

            // Retrieve all departments from the database
            var Departments = await _context.Departments.ToListAsync() ;

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
                //SearchButtonDepartment = departmentButtonName,
            };

            return View(vm);
        }

        private int GetDepartmentId(string departmentName)
        {
            return _context.Departments
                .Where(a => a.NameDepartments == departmentName)
                .Select(a => a.IDDepartments)
                .FirstOrDefault();
        }

        // GET: Schedules/Details/5
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

            return View(schedule);
        }

        // GET: Schedules/Create
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


        public async Task<IActionResult> Afluencias(int procuraLimiteAfluencia = 1,DateTime? procuraDataInicial = null, DateTime? procuraDataFinal = null )
        {
            var tickets = _context.Tickets.AsEnumerable();

            if (procuraDataInicial != null)
            {
                tickets = tickets.Where(x => x.DataEmissao >= procuraDataInicial);
            }

            if (procuraDataFinal != null)
            {
                tickets = tickets.Where(x => x.DataEmissao <= procuraDataFinal);
            }

            var departmentsWithTicketCount = _context.Departments
        .Select(d => new
        {
            Department = d,
            TicketsCount = _context.Tickets.Count(t => t.IDDepartments == d.IDDepartments &&
                                                      (!procuraDataInicial.HasValue || t.DataEmissao >= procuraDataInicial) &&
                                                      (!procuraDataFinal.HasValue || t.DataEmissao <= procuraDataFinal))
        })
        .Where(joinResult => joinResult.TicketsCount >= procuraLimiteAfluencia)
        .ToList();

            var model = new List<AfluenciasViewModel>();

            foreach (var result in departmentsWithTicketCount)
            {
                var am = new AfluenciasViewModel
                {
                    Department = result.Department,
                    Tickets = tickets
                        .Where(t => t.IDDepartments == result.Department.IDDepartments &&
                                    (!procuraDataInicial.HasValue || t.DataEmissao >= procuraDataInicial) &&
                                    (!procuraDataFinal.HasValue || t.DataEmissao <= procuraDataFinal))
                        .OrderBy(t => t.DataEmissao)
                        .ToList(),
                    SearchDataIntervaloInicial = procuraDataInicial,
                    SearchDataIntervaloFinal = procuraDataFinal
                };

                model.Add(am);
            }

            return View(model);

        }
    }
    }

