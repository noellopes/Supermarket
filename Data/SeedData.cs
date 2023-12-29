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
            PopulateHallway(db);
            PopulateShelf(db);
            PopulateShelft_ProductExhibition(db);
            PopulateWarehouse(db);
            PopulateWarehouseSection(db);
            PopulateSupplier(db);
            PopulateWarehouseSection_Product(db);
            PopulateReduceProduct(db);
            //PopulateEmployees(db);
            //PopulateEmployeeEvaluations(db);
        }

        private static void PopulateBrand(SupermarketDbContext db)
        {
            if (db.Brand.Any()) return;

            db.Brand.AddRange(
                    new Brand { Name = "Nivea" },
                    new Brand { Name = "Frankfurt" },
                    new Brand { Name = "Lays" },
                    new Brand { Name= "Monopoly" }
                );

            db.SaveChanges();
        }

        private static void PopulateCategory(SupermarketDbContext db)
        {
            if (db.Category.Any()) return;

            db.Category.AddRange(
                    new Category { Name = "Hygiene" },
                    new Category { Name = "Canned" },
                    new Category { Name = "Drinks" },
                     new Category { Name = "Games" }
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
                        Category = db.Category.FirstOrDefault(a => a.Name == "Games")!,
                        Brand = db.Brand.FirstOrDefault(a => a.Name == "Monopoly")!,
                        Name = "Monopoly Chance",
                        Description = "Family Game",
                        TotalQuantity = 26,
                        MinimumQuantity = 10,
                        UnitPrice = 25.99,
                        Status = "Available"
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
                        Product = db.Product.FirstOrDefault(a => a.Name == "Cream" && a.Description == "Skin cream.")!,
                        Shelf = db.Shelf.FirstOrDefault(a => a.Name == "Shelft 12")!,
                        Quantity = 0,
                        MinimumQuantity = 20
                    },
                    new Shelft_ProductExhibition
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Monopoly Chance" && a.Description == "Family Game")!,
                        Shelf = db.Shelf.FirstOrDefault(a => a.Name == "Shelft 11")!,
                        Quantity = 11,
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
                        BatchNumber = "D45",
                        ExpirationDate = DateTime.Now,
                        Quantity = 40,
                        ReservedQuantity = 0,
                        Supplier = db.Supplier.FirstOrDefault(a => a.Name == "Supplier Guarda")
                    },

                     new WarehouseSection_Product
                     {
                         Product = db.Product.FirstOrDefault(a => a.Name == "Monopoly Chance" && a.Description == "Family Game")!,
                         WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section A1")!,
                         BatchNumber = "R45",
                         ExpirationDate = DateTime.Now,
                         Quantity = 10,
                         ReservedQuantity = 3,
                         Supplier = db.Supplier.FirstOrDefault(a => a.Name == "Supplier Guarda")
                     },

                     new WarehouseSection_Product
                     {
                         Product = db.Product.FirstOrDefault(a => a.Name == "Monopoly Chance" && a.Description == "Family Game")!,
                         WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section A1")!,
                         BatchNumber = "Q45",
                         ExpirationDate = DateTime.Now,
                         Quantity = 5,
                         ReservedQuantity = 1,
                         Supplier = db.Supplier.FirstOrDefault(a => a.Name == "Supplier Guarda")
                     },

                     new WarehouseSection_Product
                     {
                         Product = db.Product.FirstOrDefault(a => a.Name == "Chips" && a.Description == "Ham-flavored chips.")!,
                         WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section A1")!,
                         BatchNumber = "C65",
                         ExpirationDate = DateTime.Now,
                         Quantity = 88,
                         ReservedQuantity = 0,
                         Supplier = db.Supplier.FirstOrDefault(a => a.Name == "Supplier Guarda")
                     },



                    new WarehouseSection_Product
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Sausages" && a.Description == "German Sausages.")!,
                        WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section B4")!,
                        BatchNumber = "B65",
                        ExpirationDate = DateTime.Now,
                        Quantity = 30,
                        ReservedQuantity = 10,
                        Supplier = db.Supplier.FirstOrDefault(a => a.Name == "Supplier Algarve")
                    },
                    new WarehouseSection_Product
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Chips" && a.Description == "Ham-flavored chips.")!,

                        WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section D6")!,
                        BatchNumber = "A45",
                        ExpirationDate = DateTime.Now,
                        Quantity = 15,
                        ReservedQuantity = 15,
                        Supplier = db.Supplier.FirstOrDefault(a => a.Name == "Supplier Paris")
                    }
                );

            db.SaveChanges();
        }

        private static void PopulateSupplier(SupermarketDbContext db)
        {
            if (db.Supplier.Any()) return;

            db.Supplier.AddRange(
                    new Supplier
                    {
                        Name = "Supplier Guarda",

                    },
                    new Supplier
                    {
                        Name = "Supplier Algarve",

                    },
                    new Supplier
                    {
                        Name = "Supplier Paris",

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
