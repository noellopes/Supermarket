using Microsoft.AspNetCore.Mvc;
using Supermarket.Data;
using Supermarket.Models;
using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;




namespace Supermarket.Controllers
{
    public class ControloHorasExtra : Controller
    {
        private readonly SupermarketDbContext _context;
        public ControloHorasExtra(SupermarketDbContext context)
        {
            _context = context;
        }

        public IActionResult HorasExtra()
        {
            var pontos = _context.Ponto.ToList();//Obter todos os registos de Ponto da base de dados
            return View(pontos);
        }

        [NonAction]
        public string CalcularHorasExtras(Ponto ponto)
        {
            if (ponto.CheckInTime != default(TimeSpan) && ponto.CheckOutTime != default(TimeSpan))
            {
                var horasTrabalhadas = ponto.CheckOutTime - ponto.CheckInTime;

                //Definir um limite para horas regulares (exemplo:8 horas)

                var horasRegulares = TimeSpan.FromHours(8);

                //Calcular horas extras (se excederem as horas regulares)
                var horasExtras = horasTrabalhadas > horasRegulares ? horasTrabalhadas - horasRegulares : TimeSpan.Zero;
                return horasExtras.ToString(@"hh\:mm");
            }
            return "Não há registo de CheckIn ou CheckOut";
        }
    }
}
