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
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2028, 04, 30,12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = DateTime.Now, IDDepartments = 2 },
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2024, 04, 30, 12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = DateTime.Now, IDDepartments = 1 },
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2025, 04, 30, 12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = DateTime.Now, IDDepartments = 3 },
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2028, 04, 30, 12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = DateTime.Now, IDDepartments = 4 },
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2029, 04, 30, 12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = DateTime.Now, IDDepartments = 6 }
                );

            db.SaveChanges();
        }

    }
}
