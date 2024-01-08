using Supermarket.Models;
using System;

namespace Supermarket.Data
{
    public class SeedData
    {

        internal static void Populate(SupermarketDbContext db)
        {
            PopulateDepartment(db);
            PopulateSchedules(db);
            PopulateTickets(db);

        }
        private static void PopulateDepartment(SupermarketDbContext db)
        {
            if (db.Departments.Any()) return;

            db.Departments.AddRange(
                new Department { NameDepartments = "Talho", DescriptionDepartments = "Servir carne fresca", StateDepartments = true, SkillsDepartments = "A1", QuatDepMed = 10 },
                new Department { NameDepartments = "Peixaria", DescriptionDepartments = "Servir peixe Fresco", StateDepartments = true, SkillsDepartments = "B1", QuatDepMed = 5 },
                new Department { NameDepartments = "Take-Way", DescriptionDepartments = "Servir comida Pronta", StateDepartments = false, SkillsDepartments = "Q1", QuatDepMed = 15 },
                new Department { NameDepartments = "Armazem", DescriptionDepartments = "Só porque o pinela quer ", StateDepartments = true, SkillsDepartments = "R1", QuatDepMed = 0 },
                new Department { NameDepartments = "Garrafeira", DescriptionDepartments = "Bebidas acoolicas", StateDepartments = false, SkillsDepartments = "R1", QuatDepMed = 0 },
                new Department { NameDepartments = "Congelados", DescriptionDepartments = "Produtos congelados", StateDepartments = false, SkillsDepartments = "R1", QuatDepMed = 0 },
                new Department { NameDepartments = "Legume e frutas", DescriptionDepartments = "Produtos Frescos", StateDepartments = true, SkillsDepartments = "R1", QuatDepMed = 0 },
                new Department { NameDepartments = "Padaria e Pastelaria", DescriptionDepartments = "Servir Pão", StateDepartments = false, SkillsDepartments = "S1", QuatDepMed = 15 }
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
        }
        private static void PopulateTickets(SupermarketDbContext db)
        {
            if (db.Tickets.Any()) return;
            // Instantiate random number generator using system-supplied value as seed.
            var rand = new Random();

            for (int i = 0; i < 200; i++)
            {
                var randomBool = rand.Next(2) == 1;
                db.Add(
                    new Ticket
                    {
                        DataEmissao = DateTime.Now,
                        DataAtendimento = DateTime.Now.AddMinutes(rand.Next(5,20)),
                        NumeroDaSenha = i,
                        Estado = true,
                        Prioritario = randomBool,
                        IDDepartments = rand.Next(1, db.Departments.Count())
                    }
                    );
            }

       
            db.SaveChanges();
        }
    }
}