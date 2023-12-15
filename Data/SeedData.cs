
namespace Supermarket.Data
{
    public class SeedData
    {
        internal static void Populate(SupermarketDbContext db)
        {
            PopulateType(db);
            PopulateExp(db);
        }

        private static void PopulateType(SupermarketDbContext db)
        {
            if (db.IssueType.Any())
            {
                return;
            }

            if(db.ProductExpiration.Any())
            {
                return;
            }

            db.SaveChanges();
        }

        private static void PopulateExp(SupermarketDbContext db)
        {
            db.IssueType.AddRange(
                new Models.IssueType { Name = "Expired Products", IssueDescription = "Products being sold after the expiration date" },
                new Models.IssueType { Name = "Deterioration and Damage", IssueDescription = "Products damaged during transportation, storage, or handling, compromising their quality" },
                new Models.IssueType { Name = "Cross-Contamination", IssueDescription = "Transfer of contaminants between products, often related to improper handling or storage" }
            );

            db.ProductExpiration.AddRange(
                new Models.ProductExpiration { BatchNumber = "LOTE123", ExpirationDate = new DateTime(), Quantity = 2 }
             );

            db.SaveChanges();
        }
    }
}


