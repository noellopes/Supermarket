using Supermarket.Models;

namespace Supermarket.Data
{
    public class SeedData
    {

        internal static void Populate(SupermarketDbContext db)
        {
            PopulateDepartments(db);
            PopulateSchedules(db);
            PopulateTickets(db);

        }
        private static void PopulateDepartments(SupermarketDbContext db)
        {
            if (db.Departments.Any()) return;

            db.Departments.AddRange(
                new Departments { NameDepartments = "Talho", DescriptionDepartments = "Servir carne fresca", StateDepartments = true, SkillsDepartments = "A1", QuatDepMed = 10 },
                new Departments { NameDepartments = "Peixaria", DescriptionDepartments = "Servir peixe Fresco", StateDepartments = true, SkillsDepartments = "B1", QuatDepMed = 5 },
                new Departments { NameDepartments = "Take-Way", DescriptionDepartments = "Servir comida Pronta", StateDepartments = false, SkillsDepartments = "Q1", QuatDepMed = 15 },
                new Departments { NameDepartments = "Armazem", DescriptionDepartments = "Só porque o pinela quer ", StateDepartments = true, SkillsDepartments = "R1", QuatDepMed = 0 },
                new Departments { NameDepartments = "Garrafeira", DescriptionDepartments = "Bebidas acoolicas", StateDepartments = false, SkillsDepartments = "R1", QuatDepMed = 0 },
                new Departments { NameDepartments = "Congelados", DescriptionDepartments = "Produtos congelados", StateDepartments = false, SkillsDepartments = "R1", QuatDepMed = 0 },
                new Departments { NameDepartments = "Legume e frutas", DescriptionDepartments = "Produtos Frescos", StateDepartments = true, SkillsDepartments = "R1", QuatDepMed = 0 },
                new Departments { NameDepartments = "Padaria e Pastelaria", DescriptionDepartments = "Servir Pão", StateDepartments = false, SkillsDepartments = "S1", QuatDepMed = 15 }
                );
            db.SaveChanges();
        }
        private static void PopulateSchedules(SupermarketDbContext db)
        {
            if (db.Schedule.Any()) return;

            db.Schedule.AddRange(
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2028, 04, 30, 12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = DateTime.Now, IDDepartments = db.Departments.Where(a => a.NameDepartments == "Peixaria").Select(a => a.IDDepartments).FirstOrDefault() },
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2025, 04, 30, 12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = DateTime.Now, IDDepartments = db.Departments.Where(a => a.NameDepartments == "Talho").Select(a => a.IDDepartments).FirstOrDefault() },
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2028, 04, 30, 12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = DateTime.Now, IDDepartments = db.Departments.Where(a => a.NameDepartments == "Take-Way").Select(a => a.IDDepartments).FirstOrDefault() },
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2029, 04, 30, 12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = DateTime.Now, IDDepartments = db.Departments.Where(a => a.NameDepartments == "Congelados").Select(a => a.IDDepartments).FirstOrDefault() },
                    new Schedule { StartDate = DateTime.Now, EndDate = null, DailyStartTime = DateTime.Now, DailyFinishTime = DateTime.Now, IDDepartments = db.Departments.Where(a => a.NameDepartments == "Armazem").Select(a => a.IDDepartments).FirstOrDefault() }
                );

            //for (int i = 0; i < 1000; i++)
            //{
            //    db.Add(
            //        new Schedule
            //        {
            //            StartDate = DateTime.Now,
            //            EndDate = new DateTime(2029, 04, 30, 12, 30, 0),
            //            DailyStartTime = DateTime.Now,
            //            DailyFinishTime = DateTime.Now,
            //            IDDepartments = db.Departments.Where(a => a.NameDepartments == "Armazem").Select(a => a.IDDepartments).FirstOrDefault()
            //        }
            //    );
            //}

            db.SaveChanges();
            db.SaveChanges();
        }
        private static void PopulateTickets(SupermarketDbContext db)
        {
            if (db.Tickets.Any()) return;

            db.Tickets.AddRange(
               new Tickets { DataEmissao = DateTime.Now, DataAtendimento = new DateTime(2028, 04, 30, 12, 30, 0), NumeroDaSenha = 1, Estado = true, Prioritario = false, IDDepartments = db.Departments.Where(a => a.NameDepartments == "Peixaria").Select(a => a.IDDepartments).FirstOrDefault() }
            );
            db.SaveChanges();
        }
    }
}