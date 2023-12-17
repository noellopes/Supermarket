using Microsoft.EntityFrameworkCore;
using Supermarket.Models;

namespace Supermarket.Data
{
    public class SeedData
    {
        internal static void Populate(SupermarketDbContext db)
        {
            PopulateConfSub(db);
            PopulateEmployee(db);
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

        private static void PopulateEmployee(SupermarketDbContext db)
        {
            if (db.Employee.Any()) return;

            db.Employee.AddRange(
                new Employee { Employee_Name = "Afonso", Employee_Email = "Afonso@gmail.com", Employee_Password = "afonso123", Employee_Phone = "123456789", Employee_NIF = "987654321", Employee_Address = "Rua da esquerda", Employee_Birth_Date = new DateTime(1998, 04, 23), Employee_Admission_Date = new DateTime(2023,12,17), Employee_Termination_Date = null, Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "13:30", Employee_Time_Bank = 7 },
                new Employee { Employee_Name = "Jessica", Employee_Email = "Jessica@gmail.com", Employee_Password = "Jessica123", Employee_Phone = "837462856", Employee_NIF = "875436712", Employee_Address = "Rua da direita", Employee_Birth_Date = new DateTime(2003, 04, 23), Employee_Admission_Date = new DateTime(2020 , 12 , 17), Employee_Termination_Date = null, Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "13:30", Employee_Time_Bank = 7 },
                new Employee { Employee_Name = "Hugo", Employee_Email = "Hugo@gmail.com", Employee_Password = "hugo123", Employee_Phone = "975620959", Employee_NIF = "938475610", Employee_Address = "Rua da meio", Employee_Birth_Date = new DateTime(2000, 12, 23), Employee_Admission_Date = new DateTime(2019 , 12 , 17), Employee_Termination_Date = new DateTime(2022,10,03), Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "13:30", Employee_Time_Bank = 7 },
                new Employee { Employee_Name = "Alberto", Employee_Email = "Alberto@gmail.com", Employee_Password = "Alberto123", Employee_Phone = "849257712", Employee_NIF = "098749084", Employee_Address = "Rua de cima", Employee_Birth_Date = new DateTime(2001, 04, 01), Employee_Admission_Date = new DateTime(2022 , 01 , 01), Employee_Termination_Date = null, Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "13:30", Employee_Time_Bank = 7 }

                );

            db.SaveChanges();
        }

    }
}
