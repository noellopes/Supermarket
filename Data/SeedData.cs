using Supermarket.Models;

namespace Supermarket.Data
{
    public class SeedData
    {

        internal static void Populate(SupermarketDbContext db)
        {
            PopulateSchedules(db);
            PopulateDepartments(db);
        }

        private static void PopulateSchedules(SupermarketDbContext db)
        {
            if (db.Schedule.Any()) return;

            db.Schedule.AddRange(
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2028, 04, 30,12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = DateTime.Now, IDDepartments = 2 },
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2024, 04, 30, 12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = DateTime.Now, IDDepartments = 1 },
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2025, 04, 30, 12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = DateTime.Now, IDDepartments = 3 },
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2028, 04, 30, 12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = DateTime.Now, IDDepartments = 4 },
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2029, 04, 30, 12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = DateTime.Now, IDDepartments = 6 },
                    new Schedule { StartDate = DateTime.Now, EndDate = null, DailyStartTime = DateTime.Now, DailyFinishTime = DateTime.Now, IDDepartments = 6 }
                );
            db.SaveChanges();
        }
        private static void PopulateDepartments(SupermarketDbContext db)
        {
            if (db.Departments.Any()) return;

            db.Departments.AddRange(
                new Departments { IDDepartments=10, NameDepartments = "Talho", DescriptionDepartments = "Servir carne fresca", StateDepartments =true, SkillsDepartments="A1", QuatDepMed =10},
                new Departments { IDDepartments = 20, NameDepartments = "Peixaria", DescriptionDepartments = "Servir peixe Fresco", StateDepartments = true, SkillsDepartments = "B1", QuatDepMed = 5 },
                new Departments { IDDepartments = 30, NameDepartments = "Take-Way", DescriptionDepartments = "Servir comida Pronta", StateDepartments = false, SkillsDepartments = "Q1", QuatDepMed = 15 },
                new Departments { IDDepartments = 40, NameDepartments = "Armazem", DescriptionDepartments = "Só porque o pinela quer ", StateDepartments = true, SkillsDepartments = "R1", QuatDepMed = 0 },
                new Departments { IDDepartments = 50, NameDepartments = "Garrafeira", DescriptionDepartments = "Bebidas acoolicas", StateDepartments = false , SkillsDepartments = "R1", QuatDepMed = 0 },
                new Departments { IDDepartments = 60, NameDepartments = "Congelados", DescriptionDepartments = "Produtos congelados", StateDepartments = false, SkillsDepartments = "R1", QuatDepMed = 0 },
                new Departments { IDDepartments = 70, NameDepartments = "Legume e frutas", DescriptionDepartments = "Produtos Frescos", StateDepartments = true, SkillsDepartments = "R1", QuatDepMed = 0 },
                new Departments { IDDepartments = 80, NameDepartments = "Padaria e Pastelaria", DescriptionDepartments = "Servir Pão", StateDepartments = false, SkillsDepartments = "S1", QuatDepMed = 15 }
            );

            db.SaveChanges();
        }
    }
}
