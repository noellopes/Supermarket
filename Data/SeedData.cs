using Microsoft.EntityFrameworkCore;
using Supermarket.Models;

namespace Supermarket.Data
{
    public class SeedData
    {
        internal static void Populate(SupermarketDbContext db)
        {
            PopulateConfSub(db);
        }

        private static void PopulateConfSub(SupermarketDbContext db)
        {
            if (db.SubsidySetup.Any()) return;

            db.SubsidySetup.AddRange(
               
                new SubsidySetup { HorasMinTrabalhadas = 8.5f, ValorSubsidioDiario = 5, DataEntradaVigor = new DateTime(2024, 01, 01, 00, 00, 00, 500) },
                new SubsidySetup { HorasMinTrabalhadas = 8.5f, ValorSubsidioDiario = 5, DataEntradaVigor = new DateTime(2024, 01, 01, 00, 00, 00, 500) },
                new SubsidySetup { HorasMinTrabalhadas = 8.5f, ValorSubsidioDiario = 5, DataEntradaVigor = new DateTime(2024, 01, 01, 00, 00, 00, 500) },
                new SubsidySetup { HorasMinTrabalhadas = 8.5f, ValorSubsidioDiario = 5, DataEntradaVigor = new DateTime(2024, 01, 01, 00, 00, 00, 500) }

               );

            db.SaveChanges();
        }

    }
}
