using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;
using System.Linq;
using System.Threading.Tasks;

public class SemaforoController : Controller
{
    private readonly ApplicationDbContext _context;

    public SemaforoController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var quantidadeSenhasPorDepartamento = await _context.Semaforo
            .Include(s => s.Department)
            .Where(s => s.Ticket.DataEmissao != null && s.Ticket.DataAtendimento == null)
            .GroupBy(s => s.IDDepartments)
            .Select(g => new
            {
                DepartmentId = g.Key,
                QuantidadeSenhasNaoAtendidas = g.Sum(s => s.NSenhasNoDepartamento)
            })
            .ToListAsync();

        return View(quantidadeSenhasPorDepartamento);
    }
}