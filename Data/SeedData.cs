using Microsoft.EntityFrameworkCore;
using Supermarket.Models;

namespace Supermarket.Data
{
    public class SeedData
    {
        public static void Populate(SupermarketDbContext db)
        {
            PopulateConfSub(db);
        }

        private static void PopulateConfSub(SupermarketDbContext db)
        {
            if (db.SubsidySetup.Any()) return;

            db.SubsidySetup.AddRange(
                new SubsidySetup { HorasMinTrabalhadas = new DateTime(2010, 8, 18, 16, 32, 18, 500), valorSubsidioDiario=5, DataPagamentoMensal = new DateTime(2010, 8, 18, 16, 32, 18, 500) },
                new SubsidySetup { HorasMinTrabalhadas = new DateTime(2010, 8, 18, 16, 32, 18, 500), valorSubsidioDiario = 5, DataPagamentoMensal = new DateTime(2010, 8, 18, 16, 32, 18, 500) }

               );

            db.SaveChanges();
        }

    }
}
