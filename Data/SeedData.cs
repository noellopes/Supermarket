using Supermarket.Models;

namespace Supermarket.Data
{
    public class SeedData
    {
        internal static void Populate(SupermarketDbContext db)
        {
            PopulateBrand(db);
            PopulateCategory(db);
            PopulateProduct(db);
            PopulateStore(db);
            PopulateSupplier(db);
            PopulateEmployee(db);
            PopulateMealCard(db);
            //PopulateHallway(db);
            //PopulateShelf(db);
            //PopulateShelft_ProductExhibition(db);
            //PopulateWarehouse(db);
            //PopulateWarehouseSection(db);
            //PopulateWarehouseSection_Product(db);
            //PopulateReduceProduct(db);

            //Nosso
            PopulateType(db);
            PopulateIssueType(db);
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

        private static void PopulateIssueType(SupermarketDbContext db)
        {
            db.IssueType.AddRange(
                new Models.IssueType { Name = "Cross-Contamination", IssueDescription = "Transfer of contaminants between products, often related to improper handling or storage" },
                new Models.IssueType { Name = "Customer Complaints", IssueDescription = "Issues reported by customers regarding the quality or safety of products" },
                new Models.IssueType { Name = "Deterioration and Damage", IssueDescription = "Products damaged during transportation, storage, or handling, compromising their quality" },
                //new Models.IssueType { Name = "Expired Products", IssueDescription = "Products being sold after the expiration date" },
                new Models.IssueType { Name = "Inadequate Storage Conditions", IssueDescription = "Products stored in conditions that may affect their quality, such as temperature or humidity issues" },
                new Models.IssueType { Name = "Incorrect Pricing", IssueDescription = "Mismatch between the displayed price and the actual price at the point of sale" },
                new Models.IssueType { Name = "Mislabeling", IssueDescription = "Incorrect or misleading product labeling, including inaccurate nutritional information or allergen details" },
                new Models.IssueType { Name = "Out-of-Stock", IssueDescription = "Products unavailable for purchase despite being listed as in-stock" },
                new Models.IssueType { Name = "Sanitation and Cleanliness", IssueDescription = "Concerns related to the cleanliness and hygiene of the supermarket premises" }
              );

            

             db.ProductExpiration.AddRange(
                new Models.ProductExpiration { ProductId = 1, BatchNumber = "LOTE123", ExpirationDate = new DateTime(), Quantity = 2 }
             );
            
            db.SaveChanges();
        }
        
        private static void PopulateSupplier(SupermarketDbContext db)
        {
            db.Suppliers.AddRange(
                new Models.Supplier { Name = "Global Foods Ltd" },
                new Models.Supplier { Name = "Fresh Harvest Farms" },
                new Models.Supplier { Name = "Pacific Seafood Co" },
                new Models.Supplier { Name = "Quality Meats Inc" },
                new Models.Supplier { Name = "Green Valley Grocers" },
                new Models.Supplier { Name = "Sunrise Bakeries" },
                new Models.Supplier { Name = "Golden Beverages LLC" },
                new Models.Supplier { Name = "Nature's Best Produce" },
                new Models.Supplier { Name = "Sweet Treats Confections" },
                new Models.Supplier { Name = "Sunny Farms Dairy" },
                new Models.Supplier { Name = "Premium Pet Supplies" },
                new Models.Supplier { Name = "Tech Gadgets Distributors" },
                new Models.Supplier { Name = "Office Essentials Co" },
                new Models.Supplier { Name = "Home Goods Wholesale" }
            );
            
            db.SaveChanges();
        }

        private static void PopulateMealCard(SupermarketDbContext db)
        {
            db.MealCards.AddRange(
                new Models.MealCard{
                    //MealCardId = 123,
                    Balance = 100,
                    EmployeeId = 1,
                }
            );

            db.SaveChanges();
        }

        private static void PopulateEmployee(SupermarketDbContext db)
        {
            db.Funcionarios.AddRange(
                new Models.Employee
                {
                    //EmployeeId = 1,
                    Employee_Address = "123 Main Street, Cityville",
                    Employee_Admission_Date = DateTime.Parse("2023-01-15"),
                    Employee_Birth_Date = DateTime.Parse("1990-05-20"),
                    Employee_Email = "john.doe@example.com",
                    Employee_Name = "John Doe",
                    Employee_NIF = "123456789",
                    Employee_Password = "password123",
                    Employee_Phone = "555-1234",
                    Employee_Time_Bank = DateTime.Parse("2023-05-20"),
                    Hora_Almoco_Padrao = "12:00",
                    Standard_Check_In_Time = "09:00",
                    Standard_Check_Out_Time = "17:00",
                    Standard_Lunch_Time = "01:00"
                },
                new Models.Employee
                {
                    //EmployeeId = 2,
                    Employee_Address = "456 Oak Avenue, Townsville",
                    Employee_Admission_Date = DateTime.Parse("2022-11-05"),
                    Employee_Birth_Date = DateTime.Parse("1985-08-12"),
                    Employee_Email = "jane.smith@example.com",
                    Employee_Name = "Jane Smith",
                    Employee_NIF = "987654321",
                    Employee_Password = "securepass789",
                    Employee_Phone = "555-5678",
                    Employee_Time_Bank = DateTime.Parse("2022-12-15"),
                    Hora_Almoco_Padrao = "12:30",
                    Standard_Check_In_Time = "08:30",
                    Standard_Check_Out_Time = "16:30",
                    Standard_Lunch_Time = "01:00"
                },
                new Models.Employee
                {
                    //EmployeeId = 3,
                    Employee_Address = "789 Pine Street, Villagetown",
                    Employee_Admission_Date = DateTime.Parse("2022-09-10"),
                    Employee_Birth_Date = DateTime.Parse("1982-03-25"),
                    Employee_Email = "robert.james@example.com",
                    Employee_Name = "Robert James",
                    Employee_NIF = "654321987",
                    Employee_Password = "securepassword456",
                    Employee_Phone = "555-8765",
                    Employee_Time_Bank = DateTime.Parse("2022-10-25"),
                    Hora_Almoco_Padrao = "12:15",
                    Standard_Check_In_Time = "09:15",
                    Standard_Check_Out_Time = "17:15",
                    Standard_Lunch_Time = "01:00"
                },
                new Models.Employee
                {
                    //EmployeeId = 4,
                    Employee_Address = "567 Maple Avenue, Hamletville",
                    Employee_Admission_Date = DateTime.Parse("2023-03-08"),
                    Employee_Birth_Date = DateTime.Parse("1988-11-08"),
                    Employee_Email = "susan.white@example.com",
                    Employee_Name = "Susan White",
                    Employee_NIF = "789012345",
                    Employee_Password = "pass123word",
                    Employee_Phone = "555-4321",
                    Employee_Time_Bank = DateTime.Parse("2023-04-20"),
                    Hora_Almoco_Padrao = "12:45",
                    Standard_Check_In_Time = "08:45",
                    Standard_Check_Out_Time = "16:45",
                    Standard_Lunch_Time = "01:00"
                }
            );
            db.SaveChanges();
        }


        private static void PopulateBrand(SupermarketDbContext db)
        {
            if (db.Brand.Any()) return;

            db.Brand.AddRange(
                    new Brand { Name = "Coca-Cola" },
                    new Brand { Name = "Colgate" },
                    new Brand { Name = "Dove" },
                    new Brand { Name = "Frankfurt" },
                    new Brand { Name = "General Mills" },
                    new Brand { Name = "Heinz" },
                    new Brand { Name = "Huggies" },
                    new Brand { Name = "Johnson & Johnson" },
                    new Brand { Name = "Kellogg's" },
                    new Brand { Name = "Lays" },
                    new Brand { Name = "Nestle" },
                    new Brand { Name = "Nivea" },
                    new Brand { Name = "Pampers" },
                    new Brand { Name = "Procter & Gamble" },
                    new Brand { Name = "Quaker" },
                    new Brand { Name = "Tide" }
                );

            db.SaveChanges();
        }

        private static void PopulateCategory(SupermarketDbContext db)
        {
            if (db.Category.Any()) return;

            db.Category.AddRange(
                    new Category { Name = "Baby Care" },
                    new Category { Name = "Bakery" },
                    new Category { Name = "Canned" },
                    new Category { Name = "Condiments and Sauces" },
                    new Category { Name = "Dairy and Cheese" },
                    new Category { Name = "Drinks" },
                    new Category { Name = "Electronics and Appliances" },
                    new Category { Name = "Fresh Produce" },
                    new Category { Name = "Frozen Foods" },
                    new Category { Name = "Home and Kitchen" },
                    new Category { Name = "Household Cleaning" },
                    new Category { Name = "Hygiene" },
                    new Category { Name = "Meat and Seafood" },
                    new Category { Name = "Office and School Supplies" },
                    new Category { Name = "Personal Care" },
                    new Category { Name = "Pet Supplies" },
                    new Category { Name = "Snacks" }
                );

            db.SaveChanges();
        }

        private static void PopulateProduct(SupermarketDbContext db)
        {
            if (db.Product.Any()) return;

            db.Product.AddRange(
                    new Product
                    {
                        Category = db.Category.FirstOrDefault(a => a.Name == "Hygiene")!,
                        Brand = db.Brand.FirstOrDefault(a => a.Name == "Nivea")!,
                        Name = "Cream",
                        Description = "Skin cream.",
                        TotalQuantity = 0,
                        MinimumQuantity = 200,
                        UnitPrice = 5.99,
                        Status = "Unavailable"
                    },
                    new Product
                    {
                        Category = db.Category.FirstOrDefault(a => a.Name == "Canned")!,
                        Brand = db.Brand.FirstOrDefault(a => a.Name == "Frankfurt")!,
                        Name = "Sausages",
                        Description = "German Sausages.",
                        TotalQuantity = 150,
                        MinimumQuantity = 50,
                        UnitPrice = 1.49,
                        Status = "Available"
                    },
                    new Product
                    {
                        Category = db.Category.FirstOrDefault(a => a.Name == "Canned")!,
                        Brand = db.Brand.FirstOrDefault(a => a.Name == "Lays")!,
                        Name = "Chips",
                        Description = "Ham-flavored chips.",
                        TotalQuantity = 75,
                        MinimumQuantity = 50,
                        UnitPrice = 2.29,
                        Status = "Available"
                    }
                );

            db.SaveChanges();
        }

        private static void PopulateStore(SupermarketDbContext db)
        {
            if (db.Store.Any()) return;

            db.Store.AddRange(
                    new Store
                    {
                        Name = "Store Guarda",
                        Adress = "Street Number 1"
                    },
                    new Store
                    {
                        Name = "Store Algarve",
                        Adress = "Street Number 2"
                    },
                    new Store
                    {
                        Name = "Store Paris",
                        Adress = "Street Number 3"
                    }
                );

            db.SaveChanges();
        }

        private static void PopulateHallway(SupermarketDbContext db)
        {
            if (db.Hallway.Any()) return;

            db.Hallway.AddRange(
                    new Hallway
                    {
                        Description = "Hallway A1",
                        Store = db.Store.FirstOrDefault(a => a.Name == "Store Guarda")!,
                    },
                    new Hallway
                    {
                        Description = "Hallway B1",
                        Store = db.Store.FirstOrDefault(a => a.Name == "Store Algarve")!,
                    },
                    new Hallway
                    {
                        Description = "Hallway C3",
                        Store = db.Store.FirstOrDefault(a => a.Name == "Store Paris")!,
                    }
                );

            db.SaveChanges();
        }

        private static void PopulateShelf(SupermarketDbContext db)
        {
            if (db.Shelf.Any()) return;

            db.Shelf.AddRange(
                    new Shelf
                    {
                        Name = "Shelft 11",
                        Hallway = db.Hallway.FirstOrDefault(a => a.Description == "Hallway A1")!,
                    },
                    new Shelf
                    {
                        Name = "Shelft 12",
                        Hallway = db.Hallway.FirstOrDefault(a => a.Description == "Hallway B1")!,
                    },
                    new Shelf
                    {
                        Name = "Shelft 25",
                        Hallway = db.Hallway.FirstOrDefault(a => a.Description == "Hallway C3")!,
                    }
                );

            db.SaveChanges();
        }

        private static void PopulateShelft_ProductExhibition(SupermarketDbContext db)
        {
            if (db.Shelft_ProductExhibition.Any()) return;

            db.Shelft_ProductExhibition.AddRange(
                    new Shelft_ProductExhibition
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Cream" && a.Description == "Skin cream.")!,
                        Shelf = db.Shelf.FirstOrDefault(a => a.Name == "Shelft 11")!,
                        Quantity = 0,
                        MinimumQuantity = 20
                    },
                    new Shelft_ProductExhibition
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Sausages" && a.Description == "German Sausages.")!,
                        Shelf = db.Shelf.FirstOrDefault(a => a.Name == "Shelft 11")!,
                        Quantity = 30,
                        MinimumQuantity = 10
                    },
                    new Shelft_ProductExhibition
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Chips" && a.Description == "Ham-flavored chips.")!,
                        Shelf = db.Shelf.FirstOrDefault(a => a.Name == "Shelft 11")!,
                        Quantity = 15,
                        MinimumQuantity = 10
                    }
                );

            db.SaveChanges();
        }

        private static void PopulateWarehouse(SupermarketDbContext db)
        {
            if (db.Warehouse.Any()) return;

            db.Warehouse.AddRange(
                    new Warehouse
                    {
                        Name = "Warehouse Guarda",
                        Adress = "Street Number 10"
                    },
                    new Warehouse
                    {
                        Name = "Warehouse Lisboa",
                        Adress = "Street Number 20"
                    },
                    new Warehouse
                    {
                        Name = "Warehouse Madrid",
                        Adress = "Street Number 30"
                    }
                );

            db.SaveChanges();
        }

        private static void PopulateWarehouseSection(SupermarketDbContext db)
        {
            if (db.WarehouseSection.Any()) return;

            db.WarehouseSection.AddRange(
                    new WarehouseSection
                    {
                        Description = "Warehouse Section A1",
                        Warehouse = db.Warehouse.FirstOrDefault(a => a.Name == "Warehouse Guarda")!,
                    },
                    new WarehouseSection
                    {
                        Description = "Warehouse Section B4",
                        Warehouse = db.Warehouse.FirstOrDefault(a => a.Name == "Warehouse Lisboa")!,
                    },
                    new WarehouseSection
                    {
                        Description = "Warehouse Section D6",
                        Warehouse = db.Warehouse.FirstOrDefault(a => a.Name == "Warehouse Madrid")!,
                    }
                );

            db.SaveChanges();
        }

        private static void PopulateWarehouseSection_Product(SupermarketDbContext db)
        {
            if (db.WarehouseSection_Product.Any()) return;

            db.WarehouseSection_Product.AddRange(
                    new WarehouseSection_Product
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Cream" && a.Description == "Skin cream.")!,
                        WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section A1")!,
                        Quantity = 0,
                        ReservedQuantity = 0
                    },
                    new WarehouseSection_Product
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Sausages" && a.Description == "German Sausages.")!,
                        WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section B4")!,
                        Quantity = 30,
                        ReservedQuantity = 10
                    },
                    new WarehouseSection_Product
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Chips" && a.Description == "Ham-flavored chips.")!,
                        WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section D6")!,
                        Quantity = 15,
                        ReservedQuantity = 15
                    }
                );

            db.SaveChanges();
        }

        private static void PopulateReduceProduct(SupermarketDbContext db)
        {
            if (db.ReduceProduct.Any()) return;

            db.ReduceProduct.AddRange(
                    new ReduceProduct
                    {
                        Reason = "Product past its expiration date",
                        Status = "Pending",
                        Quantity = 10,
                        Product = db.Product.FirstOrDefault(a => a.Name == "Sausages" && a.Description == "German Sausages.")!,
                        WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section B4")!
                    },
                    new ReduceProduct
                    {
                        Reason = "Product past its expiration date",
                        Status = "Pending",
                        Quantity = 10,
                        Product = db.Product.FirstOrDefault(a => a.Name == "Sausages" && a.Description == "German Sausages.")!,
                        Shelf = db.Shelf.FirstOrDefault(a => a.Name == "Shelft 11")!,
                    },
                    new ReduceProduct
                    {
                        Reason = "Product past its expiration date",
                        Status = "Pending",
                        Quantity = 10,
                        Product = db.Product.FirstOrDefault(a => a.Name == "Chips" && a.Description == "Ham-flavored chips.")!,
                        WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section D6")!
                    }
                );

            db.SaveChanges();
        }
        /*
                private static void PopulateEmployees(SupermarketDbContext db)
                {
                    if (db.Employee.Any()) return;

                    db.Employee.AddRange(
                        new Employee
                        {
                            Employee_Address= "Rua das Oliveiras",
                            Employee_Admission_Date= DateTime.Now,
                            Employee_Birth_Date= DateTime.Now,
                            Employee_Email="zeD@manga.com",
                            Employee_Name="Jose",
                            Employee_NIF = "123",
                            Employee_Password = "123",
                            Employee_Phone = "123",
                            Employee_Time_Bank= DateTime.Now,
                            Standard_Lunch_Hour = "123",
                            Standard_Check_In_Time = "123",
                            Standard_Check_Out_Time = "123",
                            Standard_Lunch_Time = "123"
                        },
                        new Employee
                        {
                            Employee_Address = "Rua do azeite",
                            Employee_Admission_Date = DateTime.Now,
                            Employee_Birth_Date = DateTime.Now,
                            Employee_Email = "zeD@manga.com",
                            Employee_Name = "Maria",
                            Employee_NIF = "123",
                            Employee_Password = "123",
                            Employee_Phone = "123",
                            Employee_Time_Bank = DateTime.Now,
                            Standard_Lunch_Hour = "123",
                            Standard_Check_In_Time = "123",
                            Standard_Check_Out_Time = "123",
                            Standard_Lunch_Time = "123"
                        },
                        new Employee
                        {
                            Employee_Address = "Avenida Afonso Pena",
                            Employee_Admission_Date = DateTime.Now,
                            Employee_Birth_Date = DateTime.Now,
                            Employee_Email = "zeD@manga.com",
                            Employee_Name = "Lucas",
                            Employee_NIF = "123",
                            Employee_Password = "123",
                            Employee_Phone = "123",
                            Employee_Time_Bank = DateTime.Now,
                            Standard_Lunch_Hour = "123",
                            Standard_Check_In_Time = "123",
                            Standard_Check_Out_Time = "123",
                            Standard_Lunch_Time = "123"
                        }
                        );

                    db.SaveChanges();
                }

                private static void PopulateEmployeeEvaluations(SupermarketDbContext db)
                {
                    if (db.EmployeeEvaluation.Any()) return;

                    db.EmployeeEvaluation.AddRange(
                        new EmployeeEvaluation
                        {
                            Description= "Atendimento excelente!",
                            EmployeeId = db.Employee.First().EmployeeId,
                            GradeNumber = 8,
                        },
                        new EmployeeEvaluation
                        {
                            Description = "Muito rude...",
                            EmployeeId = db.Employee.First().EmployeeId,
                            GradeNumber = 3,
                        },
                        new EmployeeEvaluation
                        {
                            Description = "Adorei. Muito prestativo!",
                            EmployeeId = db.Employee.First().EmployeeId,
                            GradeNumber = 10,
                        }
                        );

                    db.SaveChanges();
                }*/
    }
}



