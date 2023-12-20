using Supermarket.Models;

namespace Supermarket.Data
{
    public class SeedData
    {

        internal static void Populate(SupermarketDbContext db)
        {
            PopulateSchedules(db);
            
        }

        private static void PopulateSchedules(SupermarketDbContext db)
        {

           

            if (db.Schedule.Any()) return;

            db.Schedule.AddRange(
                    new Schedule { StartDate = DateTime.Now },
                    new Schedule { EndDate = new DateTime(2024, 04, 30) },
                    new Schedule { DailyStartTime = DateTime.Now },
                    new Schedule { DailyFinishTime = DateTime.Now },
                    new Schedule { IDDepartments = 1 }
                );

            db.SaveChanges();
        }

    }
}
