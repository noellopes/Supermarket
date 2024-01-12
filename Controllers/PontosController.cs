using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize(Roles = "Funcionário")]
        public async Task<IActionResult> Index()
        {
            var funcionario = await _context.Employee.Where(x => x.Employee_Email == User.Identity.Name).FirstOrDefaultAsync();
            if (funcionario is not null)
            {
                var vm = new PontoDateViewModel
                {
                    Pontos = await _context.Ponto.Where(s => s.EmployeeId==funcionario.EmployeeId).ToListAsync(),
                    Employee = funcionario,
                    PontoDia = await _context.Ponto.Where(x => x.EmployeeId == funcionario.EmployeeId && x.Date == DateTime.Now.Date).FirstOrDefaultAsync() ?? new Ponto { DayBalance = TimeSpan.Parse("00:00"), CheckInTime = "00:00", CheckOutTime = "00:00", LunchStartTime = "00:00", LunchEndTime = "00:00" }
                };
                return View(vm);
            }
            else
            {
                return View("HRHome");
            }
        }

        [Authorize(Roles = "Funcionário")]
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

            var escala = await _context.EmployeeSchedule
                .FirstOrDefaultAsync(m => m.Date == ponto.Date && m.EmployeeId == ponto.EmployeeId);
            if (escala == null)
            {
                return NotFound();
            }

            var vm = new PontoDetailsViewModel
            {
                Ponto = ponto,
                Escala = escala
            };
            return View(vm);
        }

        [Authorize(Roles = "Funcionário")]
        public async Task<IActionResult> RegistrarPonto()
        {
            var funcionario = await _context.Employee.Where(x => x.Employee_Email == User.Identity.Name).FirstOrDefaultAsync();
            if (funcionario is not null)
            {
                var folga = await _context.Folga
                    .Where(s => s.EmployeeId == funcionario.EmployeeId && DateTime.Now.Date >= s.DataInicio.Date && DateTime.Now.Date <= s.DataFim.Date && s.Status==Folga.FolgaStatus.Aprovada)
                    .FirstOrDefaultAsync() is not null;
                var escala = await _context.EmployeeSchedule
                    .Where(x => x.EmployeeId == funcionario.EmployeeId && x.Date==DateTime.Now.Date)
                    .FirstOrDefaultAsync();
                var pontoDia = await _context.Ponto
                    .Where(x => x.EmployeeId == funcionario.EmployeeId && x.Date == DateTime.Now.Date)
                    .FirstOrDefaultAsync();
                // O primeiro ponto já foi feito
                if (pontoDia is not null)
                {
                    var escalaFimAlmoco = TimeSpan.Parse(escala.LunchStartTime).Add(TimeSpan.FromMinutes(escala.LunchTime));
                    if (pontoDia.CheckInTime is not null && pontoDia.LunchStartTime is null && pontoDia.LunchEndTime is null && pontoDia.CheckOutTime is null)
                    {
                        pontoDia.LunchStartTime = DateTime.Now.ToString("HH:mm");

                        // Verifica se funcionario esta de folga e se o horario está na margem de 5 minutos de diferença permitidos
                        if (!folga && IsNotWithinMargin(pontoDia.LunchStartTime,escala.LunchStartTime)) pontoDia.Status = "Irregular";
                        // Adiciona a quantidade de tempo que o funcionário fez a mais ou a menos do que o planejado
                        pontoDia.DayBalance = pontoDia.DayBalance.Add(TimeSpan.Parse(pontoDia.LunchStartTime) - TimeSpan.Parse(pontoDia.CheckInTime) - TimeSpan.Parse(escala.LunchStartTime) + TimeSpan.Parse(escala.CheckInTime));
                        if (pontoDia.DayBalance < TimeSpan.Zero)
                        {
                            pontoDia.DayBalancePositive = false;
                            pontoDia.DayBalance = pontoDia.DayBalance.Duration();
                        }
                    }
                    else if(pontoDia.CheckInTime is not null && pontoDia.LunchStartTime is not null && pontoDia.LunchEndTime is null && pontoDia.CheckOutTime is null)
                    {
                        pontoDia.LunchEndTime = DateTime.Now.ToString("HH:mm");

                        if (!folga && IsNotWithinMargin(pontoDia.LunchEndTime, escalaFimAlmoco.ToString(@"hh\:mm"))) pontoDia.Status = "Irregular";
                    }
                    else if (pontoDia.CheckInTime is not null && pontoDia.LunchStartTime is not null && pontoDia.LunchEndTime is not null && pontoDia.CheckOutTime is null)
                    {
                        pontoDia.CheckOutTime = DateTime.Now.ToString("HH:mm");

                        if (!folga && IsNotWithinMargin(pontoDia.CheckOutTime, escala.CheckOutTime)) pontoDia.Status = "Irregular";

                        if(pontoDia.DayBalancePositive) pontoDia.DayBalance.Add(TimeSpan.Parse(pontoDia.CheckOutTime) - TimeSpan.Parse(pontoDia.LunchEndTime) - TimeSpan.Parse(escala.CheckOutTime) + escalaFimAlmoco);
                        else pontoDia.DayBalance.Subtract(TimeSpan.Parse(pontoDia.CheckOutTime) - TimeSpan.Parse(pontoDia.LunchEndTime) - TimeSpan.Parse(escala.CheckOutTime) + escalaFimAlmoco);

                        if (pontoDia.DayBalance < TimeSpan.Zero)
                        {
                            pontoDia.DayBalancePositive = false;
                            pontoDia.DayBalance = pontoDia.DayBalance.Duration();
                        }
                        else pontoDia.DayBalancePositive = true;
                    }

                    _context.Update(pontoDia);
                    await _context.SaveChangesAsync();
                }
                // O primeiro ponto não foi feito
                else
                {
                    pontoDia = new Ponto
                    {
                        EmployeeId = funcionario.EmployeeId,
                        Date = DateTime.Now.Date,
                        CheckInTime = DateTime.Now.ToString("HH:mm"),
                        DayBalance = TimeSpan.Parse("00:00"),
                    };

                    if (!folga && IsNotWithinMargin(pontoDia.CheckInTime, escala.CheckInTime)) pontoDia.Status = "Irregular";
                    else pontoDia.Status = "Irregular";
 
                    _context.Add(pontoDia);
                    await _context.SaveChangesAsync();
                }
                if (pontoDia.Status == "Irregular") return View("Justification", pontoDia);
                var vm = new PontoDateViewModel
                {
                    Pontos = await _context.Ponto.Where(s => s.EmployeeId == funcionario.EmployeeId).ToListAsync(),
                    Employee = funcionario,
                    PontoDia = pontoDia
                };
                return View("Index",vm);
            }
            else
            {
                return View("HRHome");
            }
        }
        static bool IsNotWithinMargin(string time1, string time2)
        {
            TimeSpan difference = (TimeSpan.Parse(time1) - TimeSpan.Parse(time2)).Duration();

            return !(difference <= TimeSpan.FromMinutes(5));
        }

        [Authorize(Roles = "Funcionário")]
        public async Task<IActionResult> RegistrarJustificativa([Bind("PontoId,Justificative")] Ponto ponto)
        {
            var justificativa = ponto.Justificative;
            ponto = await _context.Ponto.Where(s => s.PontoId == ponto.PontoId).FirstOrDefaultAsync();
            if (ponto is not null)
            {
                ponto.Justificative = justificativa;
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
            return View("Index");
        }

        private bool PontoExists(int id)
        {
            return (_context.Ponto?.Any(e => e.PontoId == id)).GetValueOrDefault();
        }
    }
}