using Microsoft.AspNetCore.Identity;
using Supermarket.Models;
using System;

namespace Supermarket.Data
{
    public class SeedData
    {
        private const string ROLE_ADMIN = "Administrator";
        private const string ROLE_CLIENT = "Client";
        private const string ROLE_ADMIN1 = "Funcionário";
        private const string ROLE_ADMIN2 = "Gestor";
        private const string ROLE_ADMIN3 = "Cliente";
        private const string ROLE_STOCK_ADMIN = "Stock Administrator";
        private const string ROLE_STOCK_OP = "Stock Operator";
        private const string ROLE_MANAGER = "Manager";
        private const string ROLE_EMPLOYEER = "Employeer";
        private const string ROLE_REGISTER = "Cash Register";

        internal static void Populate(SupermarketDbContext db)
        {
            PopulateSupplier(db);
            PopulateBrand(db);
            PopulateCategory(db);
            PopulateProduct(db);
            PopulateStore(db);
            PopulateHallway(db);
            PopulateShelf(db);
            PopulateShelft_ProductExhibition(db);
            PopulateWarehouse(db);
            PopulateWarehouseSection(db);
            PopulateWarehouseSection_Product(db);
            PopulateReduceProduct(db);
            PopulateFuncao(db);
            PopulateClients(db);
            PopulateClientCard(db);
            PopulateProductDiscounts(db);
            PopulateTakeAwayCategories(db);
            PopulateTakeAwayProducts(db);
            //PopulateCustomer(db);
            //PopulateEmployees(db);
            //PopulateHierarquias(db);
            PopulateConfSub(db);
            PopulateEmployee(db);
            PopulateMealCards(db);
            PopulateCardMovements(db);
            PopulateEmployeeEvaluations(db);
            PopulateType(db);
            PopulateIssueType(db);
            PopulatePurchase(db);
            PopulateIssue(db);
            //PopulateAlerts(db);
            PopulateDepartment(db);
            PopulateSchedules(db);
            PopulateTickets(db);
            PopulateOrder(db);
            PopulatePonto(db);
            PopulateFormation(db);
            PopulateReserve(db);
            

        }
        private static void PopulateFormation(SupermarketDbContext db)
        {
            if (db.Formation.Any())
            {
                return;
            }

            db.Formation.AddRange(
                new Models.Formation { Formation_Name = "Padeiro" },
                new Models.Formation { Formation_Name = "Caixa" },
                new Models.Formation { Formation_Name = "Talhante" },
                new Models.Formation { Formation_Name = "Limpeza" },
                new Models.Formation { Formation_Name = "Peixeira" },
                new Models.Formation { Formation_Name = "Cafetaria" },
                new Models.Formation { Formation_Name = "Operador de Armazen" },
                new Models.Formation { Formation_Name = "Reposição" }
            );

            db.SaveChanges();
        }

        //internal static void PopulateHierarquias(SupermarketDbContext db)
        //{
        //    if (db.Hierarquias.Any()) return;
        //    db.Hierarquias.AddRange(
        //    new Hierarquias { SuperiorId = 1, SubordinadoId = 2 },
        //    new Hierarquias { SuperiorId = 1, SubordinadoId = 3 },
        //    new Hierarquias { SuperiorId = 1, SubordinadoId = 4 },
        //    new Hierarquias { SuperiorId = 1, SubordinadoId = 5 },
        //    new Hierarquias { SuperiorId = 2, SubordinadoId = 3 },
        //    new Hierarquias { SuperiorId = 2, SubordinadoId = 4 },
        //    new Hierarquias { SuperiorId = 3, SubordinadoId = 6 },
        //    new Hierarquias { SuperiorId = 3, SubordinadoId = 7 }
        //    );
        //    db.SaveChanges();
        //}
        private static void PopulateReserve(SupermarketDbContext db)
        {
            if (db.Reserve.Any())
            {
                return;
            }

            db.Reserve.AddRange(
                new Models.Reserve { NumeroDeFunc = 10 },
                new Models.Reserve { NumeroDeFunc = 20 },
                new Models.Reserve { NumeroDeFunc = 30 },
                new Models.Reserve { NumeroDeFunc = 40 },
                new Models.Reserve { NumeroDeFunc = 50 },
                new Models.Reserve { NumeroDeFunc = 60 },
                new Models.Reserve { NumeroDeFunc = 70 },
                new Models.Reserve { NumeroDeFunc = 80 }
                );

            db.SaveChanges();
        }
        private static void PopulateDepartment(SupermarketDbContext db)
        {
            if (db.Departments.Any()) return;

            db.Departments.AddRange(
                new Department { NameDepartments = "Talho", DescriptionDepartments = "Servir carne fresca", StateDepartments = true, SkillsDepartments = "A1", QuatDepMed = 10 },
                new Department { NameDepartments = "Peixaria", DescriptionDepartments = "Servir peixe Fresco", StateDepartments = true, SkillsDepartments = "B1", QuatDepMed = 5 },
                new Department { NameDepartments = "Take-Way", DescriptionDepartments = "Servir comida Pronta", StateDepartments = false, SkillsDepartments = "Q1", QuatDepMed = 15 },
                new Department { NameDepartments = "Armazem", DescriptionDepartments = "Repor parteleiras,", StateDepartments = true, SkillsDepartments = "R1", QuatDepMed = 1 },
                new Department { NameDepartments = "Garrafeira", DescriptionDepartments = "Bebidas acoolicas", StateDepartments = false, SkillsDepartments = "R1", QuatDepMed = 1 },
                new Department { NameDepartments = "Congelados", DescriptionDepartments = "Produtos congelados", StateDepartments = false, SkillsDepartments = "R1", QuatDepMed = 1 },
                new Department { NameDepartments = "Legume e frutas", DescriptionDepartments = "Produtos Frescos", StateDepartments = true, SkillsDepartments = "R1", QuatDepMed = 1 },
                new Department { NameDepartments = "Padaria e Pastelaria", DescriptionDepartments = "Servir Pão", StateDepartments = false, SkillsDepartments = "S1", QuatDepMed = 15 }
                );
            db.SaveChanges();
        }
        private static void PopulateSchedules(SupermarketDbContext db)
        {
            if (db.Schedule.Any()) return;

            db.Schedule.AddRange(
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2028, 04, 30, 12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = new DateTime(2028, 04, 30, 18, 30, 0), DeptID = db.Departments.Where(a => a.NameDepartments == "Peixaria").Select(a => a.IDDepartments).FirstOrDefault() },
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2025, 04, 30, 12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = new DateTime(2028, 04, 30, 15, 30, 0), DeptID = db.Departments.Where(a => a.NameDepartments == "Talho").Select(a => a.IDDepartments).FirstOrDefault() },
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2028, 04, 30, 12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = new DateTime(2028, 04, 30, 10, 30, 0), DeptID = db.Departments.Where(a => a.NameDepartments == "Take-Way").Select(a => a.IDDepartments).FirstOrDefault() },
                    new Schedule { StartDate = DateTime.Now, EndDate = new DateTime(2029, 04, 30, 12, 30, 0), DailyStartTime = DateTime.Now, DailyFinishTime = new DateTime(2028, 04, 30, 23, 30, 0), DeptID = db.Departments.Where(a => a.NameDepartments == "Congelados").Select(a => a.IDDepartments).FirstOrDefault() },
                    new Schedule { StartDate = DateTime.Now, EndDate = null, DailyStartTime = DateTime.Now, DailyFinishTime = new DateTime(2028, 04, 30, 22, 30, 0), DeptID = db.Departments.Where(a => a.NameDepartments == "Armazem").Select(a => a.IDDepartments).FirstOrDefault() }
                );

            db.SaveChanges();
        }
        private static void PopulateTickets(SupermarketDbContext db)
        {
            if (db.Tickets.Any()) return;
            // Instantiate random number generator using system-supplied value as seed.
            var rand = new Random();

            //Geração aleatoria para testes
            for (int i = 0; i <= 5000; i++)
            {
                var DataAleatoria = new DateTime(2023, 1, 1).AddMonths(rand.Next(1, 13)).AddDays(rand.Next(1, 31)).AddHours(rand.Next(1, 24)).AddMinutes(rand.Next(0, 60)).AddSeconds(rand.Next(0, 60));
                var randomBool = rand.Next(2) == 1;
                db.Add(
                    new Ticket
                    {

                        DataEmissao = DataAleatoria,
                        DataAtendimento = DataAleatoria.AddMinutes(rand.Next(0,20)),
                        NumeroDaSenha = i,
                        Estado = true,
                        Prioritario = randomBool,
                        IDDepartments = rand.Next(1, db.Departments.Count())
                    }
                    );
            }
            //Geração aleatoria para testes de afluencia no mesmo mes
            for (int i = 0; i <= 300; i++)
            {
                var DataAleatoria = new DateTime(2023, 1, 1).AddMonths(rand.Next(1, 13)).AddDays(rand.Next(1, 31)).AddHours(rand.Next(1, 24)).AddMinutes(rand.Next(0, 60)).AddSeconds(rand.Next(0, 60));
                var randomBool = rand.Next(2) == 1;
                db.Add(
                    new Ticket
                    {
                        DataEmissao = DataAleatoria,
                        DataAtendimento = DataAleatoria.AddMinutes(rand.Next(0, 20)),
                        NumeroDaSenha = 5000+i,
                        Estado = true,
                        Prioritario = randomBool,
                        IDDepartments = rand.Next(1, db.Departments.Count())
                    }
                    );
            }
            //Geração para testes de afluencia no mesmo dia
            for (int i = 0; i <= 150; i++)
            {
                var DataAleatoria = new DateTime(2023, 1, 11).AddHours(rand.Next(1, 24)).AddMinutes(rand.Next(0, 60)).AddSeconds(rand.Next(0, 60));
                var randomBool = rand.Next(2) == 1;
                db.Add(
                    new Ticket
                    {
                        DataEmissao = DataAleatoria,
                        DataAtendimento = DataAleatoria.AddMinutes(rand.Next(0, 20)),
                        NumeroDaSenha = 5300 + i,
                        Estado = true,
                        Prioritario = randomBool,
                        IDDepartments = rand.Next(1, db.Departments.Count())
                    }
                    );
            }



            db.SaveChanges();
        }

        private static void PopulateBrand(SupermarketDbContext db)
        {

            if (db.Brand.Any()) return;

            db.Brand.AddRange(

                    new Brand { Name = "Coca-Cola" },
                    new Brand { Name = "Colgate" },
                    new Brand { Name = "Dove" },
                    new Brand { Name = "General Mills" },
                    new Brand { Name = "Heinz" },
                    new Brand { Name = "Huggies" },
                    new Brand { Name = "Johnson & Johnson" },
                    new Brand { Name = "Kellogg's" },
                    new Brand { Name = "Nestle" },
                    new Brand { Name = "Pampers" },
                    new Brand { Name = "Procter & Gamble" },
                    new Brand { Name = "Quaker" },
                    new Brand { Name = "Tide" },
                    new Brand { Name = "Nivea" },
                    new Brand { Name = "Frankfurt" },
                    new Brand { Name = "Lays" },
                    new Brand { Name = "Monopoly" },
                    new Brand { Name = "Becken" }
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
                    new Category { Name = "Baby Care" },
                    new Category { Name = "Bakery" },
                    new Category { Name = "Condiments and Sauces" },
                    new Category { Name = "Dairy and Cheese" },
                    new Category { Name = "Electronics and Appliances" },
                    new Category { Name = "Fresh Produce" },
                    new Category { Name = "Frozen Foods" },
                    new Category { Name = "Home and Kitchen" },
                    new Category { Name = "Household Cleaning" },
                    new Category { Name = "Meat and Seafood" },
                    new Category { Name = "Office and School Supplies" },
                    new Category { Name = "Personal Care" },
                    new Category { Name = "Pet Supplies" },
                    new Category { Name = "Snacks" },
                    new Category { Name = "Games" },
                    new Category { Name = "House" }

                );

            db.SaveChanges();
        }


        private static void PopulateProduct(SupermarketDbContext db)
        {
            DateTime specificLastCountDate = new DateTime(2024, 1, 2);
            DateTime specificLastCountDate1 = new DateTime(2023, 12, 29);
            DateTime specificLastCountDate2 = new DateTime(2024, 1, 5);
            DateTime specificLastCountDate3 = new DateTime(2023, 3, 1);

            if (db.Product.Any()) return;

            db.Product.AddRange(
                    new Product
                    {
                        Category = db.Category.FirstOrDefault(a => a.Name == "Hygiene")!,
                        Brand = db.Brand.FirstOrDefault(a => a.Name == "Nivea")!,
                        Name = "Cream",
                        Description = "Skin cream",
                        TotalQuantity = 0,
                        MinimumQuantity = 200,
                        UnitPrice = 5.99,
                        Status = "Unavailable",
                        LastCountDate = specificLastCountDate1
                    },

                    new Product
                    {
                        Category = db.Category.FirstOrDefault(a => a.Name == "House")!,
                        Brand = db.Brand.FirstOrDefault(a => a.Name == "Becken")!,
                        Name = "Deep fryer",
                        Description = "3.5L",
                        TotalQuantity = 31,
                        MinimumQuantity = 10,
                        UnitPrice = 88.99,
                        Status = "Available",
                        LastCountDate = specificLastCountDate2
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
                        Status = "Available",
                        LastCountDate = specificLastCountDate
                    },
                    new Product
                    {
                        Category = db.Category.FirstOrDefault(a => a.Name == "Canned")!,
                        Brand = db.Brand.FirstOrDefault(a => a.Name == "Frankfurt")!,
                        Name = "Sausages",
                        Description = "German Sausages",
                        TotalQuantity = 150,
                        MinimumQuantity = 50,
                        UnitPrice = 1.49,
                        Status = "Available",
                        LastCountDate = specificLastCountDate3
                    },
                    new Product
                    {
                        Category = db.Category.FirstOrDefault(a => a.Name == "Canned")!,
                        Brand = db.Brand.FirstOrDefault(a => a.Name == "Lays")!,
                        Name = "Chips",
                        Description = "Ham-flavored chips",
                        TotalQuantity = 75,
                        MinimumQuantity = 50,
                        UnitPrice = 2.29,
                        Status = "Available",
                        LastCountDate = specificLastCountDate2
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
                        Store = db.Store.FirstOrDefault(a => a.Name == "Store Guarda")!,
                    },
                    new Hallway
                    {
                        Description = "Hallway C3",
                        Store = db.Store.FirstOrDefault(a => a.Name == "Store Guarda")!,
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
                         Name = "Shelft 21",
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

        private static void PopulateSupplier(SupermarketDbContext db)
        {
            if (db.Suppliers.Any()) return;

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

        private static void PopulateShelft_ProductExhibition(SupermarketDbContext db)
        {
            if (db.Shelft_ProductExhibition.Any()) return;

            db.Shelft_ProductExhibition.AddRange(

                    new Shelft_ProductExhibition
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Cream" && a.Description == "Skin cream")!,

                        Shelf = db.Shelf.FirstOrDefault(a => a.Name == "Shelft 11")!,
                        Quantity = 0,
                        MinimumQuantity = 20
                    },

                     new Shelft_ProductExhibition
                     {
                         Product = db.Product.FirstOrDefault(a => a.Name == "Deep fryer" && a.Description == "3.5L")!,

                         Shelf = db.Shelf.FirstOrDefault(a => a.Name == "Shelft 11")!,
                         Quantity = 0,
                         MinimumQuantity = 20
                     },

                    new Shelft_ProductExhibition
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Cream" && a.Description == "Skin cream")!,
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
                        Product = db.Product.FirstOrDefault(a => a.Name == "Chips" && a.Description == "Ham-flavored chips")!,

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
                        Product = db.Product.FirstOrDefault(a => a.Name == "Cream" && a.Description == "Skin cream")!,

                        WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section A1")!,
                        BatchNumber = "D45",
                        ExpirationDate = DateTime.Now,
                        Quantity = 120,
                        ReservedQuantity = 0,
                        Suppliers = db.Suppliers.FirstOrDefault(a => a.Name == "Sunny Farms Dairy")
                    },

                     new WarehouseSection_Product
                     {
                         Product = db.Product.FirstOrDefault(a => a.Name == "Monopoly Chance" && a.Description == "Family Game")!,
                         WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section A1")!,
                         BatchNumber = "R45",
                         ExpirationDate = DateTime.Now,
                         Quantity = 10,
                         ReservedQuantity = 3,
                         Suppliers = db.Suppliers.FirstOrDefault(a => a.Name == "Sunny Farms Dairy")
                     },

                      new WarehouseSection_Product
                      {
                          Product = db.Product.FirstOrDefault(a => a.Name == "Deep fryer" && a.Description == "3.5L")!,
                          WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section A1")!,
                          BatchNumber = "R75",
                          ExpirationDate = DateTime.Now,
                          Quantity = 10,
                          ReservedQuantity = 3,
                          Suppliers = db.Suppliers.FirstOrDefault(a => a.Name == "Sunny Farms Dairy")
                      },

                     new WarehouseSection_Product
                     {
                         Product = db.Product.FirstOrDefault(a => a.Name == "Monopoly Chance" && a.Description == "Family Game")!,
                         WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section A1")!,
                         BatchNumber = "Q45",
                         ExpirationDate = DateTime.Now,
                         Quantity = 5,
                         ReservedQuantity = 1,
                         Suppliers = db.Suppliers.FirstOrDefault(a => a.Name == "Sunny Farms Dairy")
                     },

                     new WarehouseSection_Product
                     {
                         Product = db.Product.FirstOrDefault(a => a.Name == "Chips" && a.Description == "Ham-flavored chips")!,
                         WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section A1")!,
                         BatchNumber = "C65",
                         ExpirationDate = DateTime.Now,
                         Quantity = 88,
                         ReservedQuantity = 0,
                         Suppliers = db.Suppliers.FirstOrDefault(a => a.Name == "Sunny Farms Dairy")
                     },


                    new WarehouseSection_Product
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Sausages" && a.Description == "German Sausages")!,

                        WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section B4")!,
                        BatchNumber = "B65",
                        ExpirationDate = DateTime.Now,
                        Quantity = 30,
                        ReservedQuantity = 10,
                        Suppliers = db.Suppliers.FirstOrDefault(a => a.Name == "Sunny Farms Dairy")
                    },
                    new WarehouseSection_Product
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Chips" && a.Description == "Ham-flavored chips")!,


                        WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section D6")!,
                        BatchNumber = "A45",
                        ExpirationDate = DateTime.Now,
                        Quantity = 15,
                        ReservedQuantity = 15,

                        Suppliers = db.Suppliers.FirstOrDefault(a => a.Name == "Sunny Farms Dairy")
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
                        Product = db.Product.FirstOrDefault(a => a.Name == "Sausages" && a.Description == "German Sausages")!,
                        WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section B4")!
                    },
                    new ReduceProduct
                    {
                        Reason = "Product past its expiration date",
                        Status = "Pending",
                        Quantity = 10,
                        Product = db.Product.FirstOrDefault(a => a.Name == "Sausages" && a.Description == "German Sausages")!,
                        Shelf = db.Shelf.FirstOrDefault(a => a.Name == "Shelft 11")!,
                    },
                    new ReduceProduct
                    {
                        Reason = "Product past its expiration date",
                        Status = "Pending",
                        Quantity = 10,
                        Product = db.Product.FirstOrDefault(a => a.Name == "Chips" && a.Description == "Ham-flavored chips")!,
                        WarehouseSection = db.WarehouseSection.FirstOrDefault(a => a.Description == "Warehouse Section D6")!
                    }
                );

            db.SaveChanges();
        }

        private static void PopulateAlerts(SupermarketDbContext db)
        {
            DateTime specificDate = new DateTime(2023, 1, 2);

            if (db.Alert.Any()) return;

            db.Alert.AddRange(
                    new Alert
                    {
                        Role = "Stock Operator",
                        Date = specificDate,
                        Description = "Prepare Order"
                    },
                    new Alert
                    {
                        Role = "Stock Admin",
                        Date = specificDate,
                        Description = "Reduce Product"
                    },
                    new Alert
                    {
                        Role = "Stock Admin",
                        Date = specificDate,
                        Description = "Reduce Product"
                    }
                );

            db.SaveChanges();
        }

        private static void PopulateCustomer(SupermarketDbContext db)
        {
            if (db.Customers.Any()) return;

            db.Customers.AddRange(
                    new Customer
                    {
                        CustomerAddress = "Guarda/Portugal",
                        CustomerEmail = "omeerabay@gmail.com",
                        CustomerName = "Omer",
                        CustomerPhone = "+905537466968",
                        Password = "omerabay"
                    },
                    new Customer
                    {
                        CustomerAddress = "Guarda/Portugal",
                        CustomerEmail = "yusuf@gmail.com",
                        CustomerName = "Yusuf",
                        CustomerPhone = "+905537466968",
                        Password = "yusuftasci"
                    },
                    new Customer
                    {
                        CustomerAddress = "Guarda/Portugal",
                        CustomerEmail = "melike@gmail.com",
                        CustomerName = "Melike",
                        CustomerPhone = "+905537466968",
                        Password = "melikegokdemir"
                    }
                );

            db.SaveChanges();

        }


        private static void PopulateEmployees(SupermarketDbContext db)
        {
            if (db.Employee.Any()) return;

            // db.Employee.AddRange(

            // new Employee
            //        Employee_Address= "Rua das Oliveiras",
            //        Employee_Admission_Date= DateTime.Now,
            //        Employee_Birth_Date= DateTime.Now,
            //        Employee_Email="zeD@manga.com",
            //        Employee_Name="Jose",
            //        Employee_NIF = "123",
            //        Employee_Password = "123",
            //        Employee_Phone = "123",
            //        Employee_Time_Bank= DateTime.Now,
            //        Standard_Lunch_Hour = "123",
            //        Standard_Check_In_Time = "123",
            //        Standard_Check_Out_Time = "123",
            //        Standard_Lunch_Time = "123"
            //    },
            //    new Employee
            //    {
            //        Employee_Address = "Rua do azeite",
            //        Employee_Admission_Date = DateTime.Now,
            //        Employee_Birth_Date = DateTime.Now,
            //        Employee_Email = "zeD@manga.com",
            //        Employee_Name = "Maria",
            //        Employee_NIF = "123",
            //        Employee_Password = "123",
            //        Employee_Phone = "123",
            //        Employee_Time_Bank = DateTime.Now,
            //        Standard_Lunch_Hour = "123",
            //        Standard_Check_In_Time = "123",
            //        Standard_Check_Out_Time = "123",
            //        Standard_Lunch_Time = "123"
            //    },
            //    new Employee
            //    {
            //        Employee_Address = "Avenida Afonso Pena",
            //        Employee_Admission_Date = DateTime.Now,
            //        Employee_Birth_Date = DateTime.Now,
            //        Employee_Email = "zeD@manga.com",
            //        Employee_Name = "Lucas",
            //        Employee_NIF = "123",
            //        Employee_Password = "123",
            //        Employee_Phone = "123",
            //        Employee_Time_Bank = DateTime.Now,
            //        Standard_Lunch_Hour = "123",
            //        Standard_Check_In_Time = "123",
            //        Standard_Check_Out_Time = "123",
            //        Standard_Lunch_Time = "123"
            //    }
            //    );

            //db.SaveChanges();
        }


        private static void PopulateEmployeeEvaluations(SupermarketDbContext db)
        {
            if (db.EmployeeEvaluation.Any()) return;

            db.EmployeeEvaluation.AddRange(
                new EmployeeEvaluation
                {

                    Description = "Atendimento excelente!",
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


            for (int i = 1; i < 101; i++)
            {
                db.EmployeeEvaluation.Add(new EmployeeEvaluation
                {
                    Description = "avaliacao " + i,
                    EmployeeId = db.Employee.First().EmployeeId,
                    GradeNumber = (i % 10) + 1,

                });
            }

            db.SaveChanges();
        }

        private static void PopulateFuncao(SupermarketDbContext db)
        {
            if (db.Funcao.Any()) return;


            for (int i = 0; i < 240; i++)
            {
                db.Funcao.Add(new Funcao
                {
                    NomeFuncao = "Funcao " + i,
                    DescricaoFuncao = "Descricao " + i
                });
            }
            db.SaveChanges();

        }

        private static void PopulateClients(SupermarketDbContext db)
        {
            if (db.Client.Any()) return;

            db.Client.AddRange(
                new Client
                {

                    ClientName = "João",
                    ClientAdress = "Largo do Poço",
                    ClientEmail = "joão@gmail.com",
                    ClientBirth = new DateTime(1980, 10, 20),
                    Estado = true
                },

                new Client
                {

                    ClientName = "Rui",
                    ClientAdress = "Rua do Penedo",
                    ClientEmail = "rui@sapo.com",
                    ClientBirth = new DateTime(1970, 2, 12),
                    Estado = true
                },

                new Client
                {

                    ClientName = "Jacinta",
                    ClientAdress = "Fundo da Vila",
                    ClientEmail = "jacintona@iol.com",
                    ClientBirth = new DateTime(2002, 7, 22),
                    Estado = true
                },

                new Client
                {

                    ClientName = "Hugo",
                    ClientAdress = "Casal do Rei",
                    ClientEmail = "hugo@outlook.com",
                    ClientBirth = new DateTime(1997, 9, 2),
                    Estado = true
                }
                );
            db.SaveChanges();
        }


        private static void PopulateClientCard(SupermarketDbContext db)
        {
            if (db.ClientCard.Any()) return;

            db.ClientCard.AddRange(
                new ClientCard
                {

                    Client = db.Client.FirstOrDefault(b => b.ClientName == "Hugo")!,
                    ClientCardNumber = 123456,
                    Balance = 0,
                    Estado = true
                },

                new ClientCard
                {

                    Client = db.Client.FirstOrDefault(b => b.ClientName == "Jacinta")!,
                    ClientCardNumber = 987654,
                    Balance = 0,
                    Estado = true
                },

                new ClientCard
                {

                    Client = db.Client.FirstOrDefault(b => b.ClientName == "Rui")!,
                    ClientCardNumber = 111223,
                    Balance = 0,
                    Estado = true
                }
            );

            db.SaveChanges();
        }


        private static void PopulateProductDiscounts(SupermarketDbContext db)
        {
            if (db.ProductDiscount.Any()) return;

            db.ProductDiscount.AddRange(
                new ProductDiscount
                {

                    Product = db.Product.FirstOrDefault(b => b.Name == "Cream")!,
                    ClientCard = db.ClientCard.FirstOrDefault(b => b.ClientCardNumber == 123456)!,
                    Value = 10,
                    StartDate = new DateTime(2023, 12, 17),
                    EndDate = new DateTime(2023, 12, 21)
                }
                );
            db.SaveChanges();
        }

        private static void PopulateConfSub(SupermarketDbContext db)
        {
            if (db.SubsidySetup.Any()) return;

            db.SubsidySetup.AddRange(

                new SubsidySetup { HorasMinTrabalhadas = 4f, ValorSubsidioDiario = 5, DataEntradaVigor = new DateTime(2023, 01, 01, 00, 00, 00, 500) },
                new SubsidySetup { HorasMinTrabalhadas = 4f, ValorSubsidioDiario = 6, DataEntradaVigor = new DateTime(2024, 01, 01, 00, 00, 00, 500) }

               );

            db.SaveChanges();
        }



        private static void PopulateEmployee(SupermarketDbContext db)
        {
            if (db.Employee.Any()) return;

            db.Employee.AddRange(

                new Employee { Employee_Name = "Afonso Almeida", Employee_Email = "Afonso@gmail.com", Employee_Password = "afonso123", Employee_Phone = "123456789", Employee_NIF = "987654321", Employee_Address = "Rua da esquerda", Employee_Birth_Date = new DateTime(1998, 04, 23), Employee_Admission_Date = new DateTime(2023, 12, 17), Employee_Termination_Date = null, Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "1" },

                new Employee { Employee_Name = "Jessica Azevedo", Employee_Email = "Jessica@gmail.com", Employee_Password = "Jessica123", Employee_Phone = "837462856", Employee_NIF = "875436712", Employee_Address = "Rua da direita", Employee_Birth_Date = new DateTime(2003, 04, 23), Employee_Admission_Date = new DateTime(2020, 12, 17), Employee_Termination_Date = null, Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "2" },
                new Employee { Employee_Name = "Hugo Braga", Employee_Email = "Hugo@gmail.com", Employee_Password = "hugo123", Employee_Phone = "975620559", Employee_NIF = "938475610", Employee_Address = "Rua da meio", Employee_Birth_Date = new DateTime(2000, 12, 23), Employee_Admission_Date = new DateTime(2019, 12, 17), Employee_Termination_Date = new DateTime(2022, 10, 03), Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "2" },
                new Employee { Employee_Name = "Alberto Barros", Employee_Email = "Alberto1@gmail.com", Employee_Password = "Alberto123", Employee_Phone = "843257712", Employee_NIF = "098764084", Employee_Address = "Rua de cima", Employee_Birth_Date = new DateTime(2001, 04, 01), Employee_Admission_Date = new DateTime(2022, 01, 01), Employee_Termination_Date = null, Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "1" },
                new Employee { Employee_Name = "Afonso Campos", Employee_Email = "Afonso1@gmail.com", Employee_Password = "afonso123", Employee_Phone = "123454789", Employee_NIF = "987655321", Employee_Address = "Rua da esquerda", Employee_Birth_Date = new DateTime(1998, 04, 23), Employee_Admission_Date = new DateTime(2023, 12, 17), Employee_Termination_Date = null, Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "1" },
                new Employee { Employee_Name = "Jessica Cardoso", Employee_Email = "Jessica1@gmail.com", Employee_Password = "Jessica123", Employee_Phone = "837468856", Employee_NIF = "872436712", Employee_Address = "Rua da direita", Employee_Birth_Date = new DateTime(2003, 04, 23), Employee_Admission_Date = new DateTime(2020, 12, 17), Employee_Termination_Date = null, Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "2" },
                new Employee { Employee_Name = "Hugo Correia", Employee_Email = "Hugo1@gmail.com", Employee_Password = "hugo123", Employee_Phone = "975620959", Employee_NIF = "238475610", Employee_Address = "Rua da meio", Employee_Birth_Date = new DateTime(2000, 12, 23), Employee_Admission_Date = new DateTime(2019, 12, 17), Employee_Termination_Date = new DateTime(2022, 10, 03), Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "2" },
                new Employee { Employee_Name = "Alberto Castro ", Employee_Email = "Alberto1@gmail.com", Employee_Password = "Alberto123", Employee_Phone = "849252712", Employee_NIF = "394749084", Employee_Address = "Rua de cima", Employee_Birth_Date = new DateTime(2001, 04, 01), Employee_Admission_Date = new DateTime(2022, 01, 01), Employee_Termination_Date = null, Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "1" },
                new Employee { Employee_Name = "Afonso Costa", Employee_Email = "Afonso2@gmail.com", Employee_Password = "afonso123", Employee_Phone = "143456789", Employee_NIF = "487654321", Employee_Address = "Rua da esquerda", Employee_Birth_Date = new DateTime(1998, 04, 23), Employee_Admission_Date = new DateTime(2023, 12, 17), Employee_Termination_Date = null, Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "1" },
                new Employee { Employee_Name = "Jessica Fontes", Employee_Email = "Jessica2@gmail.com", Employee_Password = "Jessica123", Employee_Phone = "827462856", Employee_NIF = "575436712", Employee_Address = "Rua da direita", Employee_Birth_Date = new DateTime(2003, 04, 23), Employee_Admission_Date = new DateTime(2020, 12, 17), Employee_Termination_Date = null, Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "2" },
                new Employee { Employee_Name = "Hugo Guimarães", Employee_Email = "Hugo2@gmail.com", Employee_Password = "hugo123", Employee_Phone = "975120959", Employee_NIF = "738475610", Employee_Address = "Rua da meio", Employee_Birth_Date = new DateTime(2000, 12, 23), Employee_Admission_Date = new DateTime(2019, 12, 17), Employee_Termination_Date = new DateTime(2022, 10, 03), Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "2" },
                new Employee { Employee_Name = "Alberto Magalhães", Employee_Email = "Alberto2@gmail.com", Employee_Password = "Alberto123", Employee_Phone = "849257702", Employee_NIF = "898749084", Employee_Address = "Rua de cima", Employee_Birth_Date = new DateTime(2001, 04, 01), Employee_Admission_Date = new DateTime(2022, 01, 01), Employee_Termination_Date = null, Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "1" },
                new Employee { Employee_Name = "Afonso Macedo", Employee_Email = "Afonso3@gmail.com", Employee_Password = "afonso123", Employee_Phone = "128256789", Employee_NIF = "444654321", Employee_Address = "Rua da esquerda", Employee_Birth_Date = new DateTime(1998, 04, 23), Employee_Admission_Date = new DateTime(2023, 12, 17), Employee_Termination_Date = null, Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "1" },
                new Employee { Employee_Name = "Jessica Matos", Employee_Email = "Jessica3@gmail.com", Employee_Password = "Jessica123", Employee_Phone = "837238856", Employee_NIF = "875242712", Employee_Address = "Rua da direita", Employee_Birth_Date = new DateTime(2003, 04, 23), Employee_Admission_Date = new DateTime(2020, 12, 17), Employee_Termination_Date = null, Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "2" },
                new Employee { Employee_Name = "Hugo Pedreira", Employee_Email = "Hugo3@gmail.com", Employee_Password = "hugo123", Employee_Phone = "975620579", Employee_NIF = "345675610", Employee_Address = "Rua da meio", Employee_Birth_Date = new DateTime(2000, 12, 23), Employee_Admission_Date = new DateTime(2019, 12, 17), Employee_Termination_Date = new DateTime(2022, 10, 03), Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "2" },
                new Employee { Employee_Name = "Alberto Queirós", Employee_Email = "Alberto3@gmail.com", Employee_Password = "Alberto123", Employee_Phone = "843467712", Employee_NIF = "045139084", Employee_Address = "Rua de cima", Employee_Birth_Date = new DateTime(2001, 04, 01), Employee_Admission_Date = new DateTime(2022, 01, 01), Employee_Termination_Date = null, Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "1" },
                new Employee { Employee_Name = "Employee", Employee_Email = "employee@ipg.pt", Employee_Password = "Secret#123", Employee_Phone = "843467712", Employee_NIF = "045139084", Employee_Address = "Rua de cima", Employee_Birth_Date = new DateTime(2001, 04, 01), Employee_Admission_Date = new DateTime(2022, 01, 01), Employee_Termination_Date = null, Standard_Check_In_Time = "9:30", Standard_Check_Out_Time = "17:30", Standard_Lunch_Hour = "12:30", Standard_Lunch_Time = "1" }
            );

            db.SaveChanges();
        }

        private static void PopulateMealCards(SupermarketDbContext db)
        {
            if (db.MealCard.Any()) return;

            db.MealCard.AddRange(
                new MealCard { EmployeeId = 1 },
                new MealCard { EmployeeId = 3 },
                new MealCard { EmployeeId = 2 },
                new MealCard { EmployeeId = 4 },
                new MealCard { EmployeeId = 6 },
                new MealCard { EmployeeId = 7 },
                new MealCard { EmployeeId = 5 },
                new MealCard { EmployeeId = 10 },
                new MealCard { EmployeeId = 9 },
                new MealCard { EmployeeId = 8 }
            );

            db.SaveChanges();
        }


        private static void PopulateCardMovements(SupermarketDbContext db)
        {
            if (db.CardMovement.Any()) return;

            db.CardMovement.AddRange(
                new CardMovement { MealCardId = 1, Description = "Pagamento Subsidio", Movement_Date = DateTime.Now, Value = 200 },
                new CardMovement { MealCardId = 1, Description = "Pagamento Subsidio", Movement_Date = DateTime.Now, Value = 150 },
                new CardMovement { MealCardId = 1, Description = "Pagamento Subsidio", Movement_Date = DateTime.Now, Value = 180 },
                new CardMovement { MealCardId = 2, Description = "Pagamento Subsidio", Movement_Date = DateTime.Now, Value = 250 },
                new CardMovement { MealCardId = 1, Description = "Compra Supermercado", Movement_Date = DateTime.Now, Value = -100 },
                new CardMovement { MealCardId = 1, Description = "Compra Supermercado", Movement_Date = DateTime.Now, Value = -120 },
                new CardMovement { MealCardId = 4, Description = "Pagamento Subsidio", Movement_Date = DateTime.Now, Value = 200 },
                new CardMovement { MealCardId = 3, Description = "Pagamento Subsidio", Movement_Date = DateTime.Now, Value = 180 },
                new CardMovement { MealCardId = 4, Description = "Compra Supermercado", Movement_Date = DateTime.Now, Value = -90 },
                new CardMovement { MealCardId = 5, Description = "Pagamento Subsidio", Movement_Date = DateTime.Now, Value = 200 },
                new CardMovement { MealCardId = 6, Description = "Pagamento Subsidio", Movement_Date = DateTime.Now, Value = 120 },
                new CardMovement { MealCardId = 6, Description = "Compra Supermercado", Movement_Date = DateTime.Now, Value = -110 },
                new CardMovement { MealCardId = 7, Description = "Pagamento Subsidio", Movement_Date = DateTime.Now, Value = 170 },
                new CardMovement { MealCardId = 8, Description = "Pagamento Subsidio", Movement_Date = DateTime.Now, Value = 210 },
                new CardMovement { MealCardId = 9, Description = "Pagamento Subsidio", Movement_Date = DateTime.Now, Value = 230 },
                new CardMovement { MealCardId = 9, Description = "Compra Supermercado", Movement_Date = DateTime.Now, Value = -130 }
            );

            db.SaveChanges();
        }

        private static void PopulateTakeAwayCategories(SupermarketDbContext db)
        {
            if (db.TakeAwayCategory.Any()) return;

            db.TakeAwayCategory.AddRange(
                    new TakeAwayCategory
                    {
                        //Id = 1,
                        Name = "Soups",
                    },
                    new TakeAwayCategory
                    {
                        //Id= 2,
                        Name = "Burgers"
                    },
                    new TakeAwayCategory
                    {
                        //Id = 3,
                        Name = "Breakfast"
                    },
                    new TakeAwayCategory
                    {
                        //Id = 4,
                        Name = "Beverages"
                    }
                );
            db.SaveChanges();
        }
        private static void PopulateTakeAwayProducts(SupermarketDbContext db)
        {
            if (db.TakeAwayProduct.Any()) return;

            db.TakeAwayProduct.AddRange(
                    new TakeAwayProduct
                    {
                        ProductName = "Coca Cola",

                        Category = db.TakeAwayCategory.FirstOrDefault(x => x.Name == "Beverages")!,

                        Price = 2,
                        Quantity = 20,
                        EstimatedPreparationTimeAsMinutes = 1,
                    }
                );
            db.SaveChanges();
        }


        internal static async void PopulateDevUsers(UserManager<IdentityUser>? userManager)
        {
            var user1 = await EnsureUserIsCreatedAsync(userManager!, "admin@ipg.pt", "Secret#123");
            var user2 = await EnsureUserIsCreatedAsync(userManager!, "anasilva_pinhel@hotmail.com", "Informatica_123");
            var user3 = await EnsureUserIsCreatedAsync(userManager!, "Afonso@gmail.com", "Afonso#123");
            var user4 = await EnsureUserIsCreatedAsync(userManager!, "Hugo@gmail.com", "Hugo#123");
            var user5 = await EnsureUserIsCreatedAsync(userManager!, "Alberto1@gmail.com", "Alberto#123");
            var user6 = await EnsureUserIsCreatedAsync(userManager!, "Afonso1@gmail.com", "AfonsoI#123");
            var user7 = await EnsureUserIsCreatedAsync(userManager!, "Jessica1@gmail.com", "JessicaI#123");
            var user8 = await EnsureUserIsCreatedAsync(userManager!, "Hugo1@gmail.com", "HugoI#123");
            var user9 = await EnsureUserIsCreatedAsync(userManager!, "Alberto2@gmail.com", "AlbertoI#123");
            var user10 = await EnsureUserIsCreatedAsync(userManager!, "Afonso2@gmail.com", "AfonsoII#123");
            var user11 = await EnsureUserIsCreatedAsync(userManager!, "Jessica2@gmail.com", "JessicaII#123");
            var user12 = await EnsureUserIsCreatedAsync(userManager!, "Hugo2@gmail.com", "HugoII#123");
            var user13 = await EnsureUserIsCreatedAsync(userManager!, "Afonso3@gmail.com", "AfonsoIII#123");
            var user14 = await EnsureUserIsCreatedAsync(userManager!, "Jessica3@gmail.com", "JessicaIII#123");
            var user15 = await EnsureUserIsCreatedAsync(userManager!, "Hugo3@gmail.com", "HugoIII#123");
            var user17 = await EnsureUserIsCreatedAsync(userManager!, "Jessica@gmail.com", "Jessica#123");
            var user18 = await EnsureUserIsCreatedAsync(userManager!, "Jessica@gmail.com", "Jessica#123");

            //Group4----------------------------------------------------------------
            var userJoaoStockAdmin = await EnsureUserIsCreatedAsync(userManager!, "joaostockadmin@ipg.pt", "Secret#123");
            var userAndreStockOp = await EnsureUserIsCreatedAsync(userManager!, "andrestockop@ipg.pt", "Secret#123");
            var userIvoStockOp = await EnsureUserIsCreatedAsync(userManager!, "ivostockop@ipg.pt", "Secret#123");

            var user = await EnsureUserIsCreatedAsync(userManager!, "admin@ipg.pt", "Secret#123");

            if (!await userManager!.IsInRoleAsync(user1, ROLE_ADMIN))
            {
                await userManager!.AddToRoleAsync(user1, ROLE_ADMIN);
            }
            if (!await userManager!.IsInRoleAsync(userJoaoStockAdmin, ROLE_STOCK_ADMIN))
            {
                await userManager!.AddToRoleAsync(userJoaoStockAdmin, ROLE_STOCK_ADMIN);
            }
            if (!await userManager!.IsInRoleAsync(userAndreStockOp, ROLE_STOCK_OP))
            {
                await userManager!.AddToRoleAsync(userAndreStockOp, ROLE_STOCK_OP);
            }
            if (!await userManager!.IsInRoleAsync(userIvoStockOp, ROLE_STOCK_OP))
            {
                await userManager!.AddToRoleAsync(userIvoStockOp, ROLE_STOCK_OP);
            }
            //Group3----------------------------------------------------------------

            var adminGroup3 = await EnsureUserIsCreatedAsync(userManager!, "admingroup3@ipg.pt", "Secret#123");
            var employeeGroup3 = await EnsureUserIsCreatedAsync(userManager!, "employeegroup3@ipg.pt", "Secret#123");


            if (!await userManager!.IsInRoleAsync(adminGroup3, ROLE_ADMIN))
            {
                await userManager!.AddToRoleAsync(adminGroup3, ROLE_ADMIN);
            }
            if (!await userManager!.IsInRoleAsync(adminGroup3, ROLE_EMPLOYEER))
            {
                await userManager!.AddToRoleAsync(adminGroup3, ROLE_EMPLOYEER);
            }
            if (!await userManager!.IsInRoleAsync(employeeGroup3, ROLE_EMPLOYEER))
            {
                await userManager!.AddToRoleAsync(employeeGroup3, ROLE_EMPLOYEER);
            }


            //Group7----------------------------------------------------------------

            var adminGroup7 = await EnsureUserIsCreatedAsync(userManager!, "adminGroup7@ipg.pt", "Secret#123");
            var employeeGroup7 = await EnsureUserIsCreatedAsync(userManager!, "employeeGroup7@ipg.pt", "Secret#123");
            var managerGroup7 = await EnsureUserIsCreatedAsync(userManager!, "managerGroup7@ipg.pt", "Secret#123");
            var registerGroup7 = await EnsureUserIsCreatedAsync(userManager!, "registerGroup7@ipg.pt", "Secret#123");

            if (!await userManager!.IsInRoleAsync(adminGroup7, ROLE_ADMIN))
            {
                await userManager!.AddToRoleAsync(adminGroup7, ROLE_ADMIN);
            }
            if (!await userManager!.IsInRoleAsync(adminGroup7, ROLE_EMPLOYEER))
            {
                await userManager!.AddToRoleAsync(adminGroup7, ROLE_EMPLOYEER);
            }
            if (!await userManager!.IsInRoleAsync(adminGroup7, ROLE_MANAGER))
            {
                await userManager!.AddToRoleAsync(adminGroup7, ROLE_MANAGER);
            }
            if (!await userManager!.IsInRoleAsync(employeeGroup7, ROLE_EMPLOYEER))
            {
                await userManager!.AddToRoleAsync(employeeGroup7, ROLE_EMPLOYEER);
            }
            if (!await userManager!.IsInRoleAsync(managerGroup7, ROLE_MANAGER))
            {
                await userManager!.AddToRoleAsync(managerGroup7, ROLE_MANAGER);
            }
            if (!await userManager!.IsInRoleAsync(managerGroup7, ROLE_EMPLOYEER))
            {
                await userManager!.AddToRoleAsync(managerGroup7, ROLE_EMPLOYEER);
            }
            if (!await userManager!.IsInRoleAsync(registerGroup7, ROLE_EMPLOYEER))
            {
                await userManager!.AddToRoleAsync(registerGroup7, ROLE_EMPLOYEER);
            }
            if (!await userManager!.IsInRoleAsync(registerGroup7, ROLE_REGISTER))
            {
                await userManager!.AddToRoleAsync(registerGroup7, ROLE_REGISTER);
            }



            //Group6----------------------------------------------------------------
            var adminGrupo6 = await EnsureUserIsCreatedAsync(userManager!, "adminGrupo6@ipg.pt", "Secret#123");
            var clientGrupo6 = await EnsureUserIsCreatedAsync(userManager!, "clientGrupo6@ipg.pt", "Secret#123");
            if (!await userManager!.IsInRoleAsync(adminGrupo6, ROLE_ADMIN))
            {
                await userManager!.AddToRoleAsync(adminGrupo6, ROLE_ADMIN);
            }
            if (!await userManager!.IsInRoleAsync(adminGrupo6, ROLE_CLIENT))
            {
                await userManager!.AddToRoleAsync(adminGrupo6, ROLE_CLIENT);
            }
            if (!await userManager!.IsInRoleAsync(clientGrupo6, ROLE_CLIENT))
            {
                await userManager!.AddToRoleAsync(clientGrupo6, ROLE_CLIENT);
            }
            //--------------------------------------------------------------------


            if (!await userManager!.IsInRoleAsync(user1, "Gestor"))
            {
                await userManager!.AddToRoleAsync(user1, "Gestor");
            }

            if (!await userManager!.IsInRoleAsync(user2, "Funcionário"))
            {
                await userManager!.AddToRoleAsync(user2, "Funcionário");
            }
            if (!await userManager!.IsInRoleAsync(user3, "Funcionário"))
            {
                await userManager!.AddToRoleAsync(user3, "Funcionário");
            }

            if (!await userManager!.IsInRoleAsync(user4, "Funcionário"))
            {
                await userManager!.AddToRoleAsync(user4, "Funcionário");
            }

            if (!await userManager!.IsInRoleAsync(user5, "Funcionário"))
            {
                await userManager!.AddToRoleAsync(user5, "Funcionário");
            }

            if (!await userManager!.IsInRoleAsync(user6, "Funcionário"))
            {
                await userManager!.AddToRoleAsync(user6, "Funcionário");
            }

            if (!await userManager!.IsInRoleAsync(user5, "Funcionário"))
            {
                await userManager!.AddToRoleAsync(user5, "Funcionário");
            }

            if (!await userManager!.IsInRoleAsync(user7, "Funcionário"))
            {
                await userManager!.AddToRoleAsync(user7, "Funcionário");
            }
            if (!await userManager!.IsInRoleAsync(user8, "Funcionário"))
            {
                await userManager!.AddToRoleAsync(user8, "Funcionário");
            }
            if (!await userManager!.IsInRoleAsync(user9, "Funcionário"))
            {
                await userManager!.AddToRoleAsync(user9, "Funcionário");
            }
            if (!await userManager!.IsInRoleAsync(user10, "Funcionário"))
            {
                await userManager!.AddToRoleAsync(user10, "Funcionário");
            }
            if (!await userManager!.IsInRoleAsync(user11, "Funcionário"))
            {
                await userManager!.AddToRoleAsync(user11, "Funcionário");
            }
            if (!await userManager!.IsInRoleAsync(user12, "Funcionário"))
            {
                await userManager!.AddToRoleAsync(user12, "Funcionário");
            }

            if (!await userManager!.IsInRoleAsync(user13, "Funcionário"))
            {
                await userManager!.AddToRoleAsync(user13, "Funcionário");
            }
            if (!await userManager!.IsInRoleAsync(user14, "Funcionário"))
            {
                await userManager!.AddToRoleAsync(user14, "Funcionário");
            }
            if (!await userManager!.IsInRoleAsync(user15, "Funcionário"))
            {
                await userManager!.AddToRoleAsync(user15, "Funcionário");
            }

            var costumer = await EnsureUserIsCreatedAsync(userManager!, "costumer@ipg.pt", "Secret#123");
            if (!await userManager!.IsInRoleAsync(costumer, "Avaliar_Funcionarios"))
            {
                await userManager!.AddToRoleAsync(costumer, "Avaliar_Funcionarios");
            }

            var employee = await EnsureUserIsCreatedAsync(userManager!, "employee@ipg.pt", "Secret#123");
            if (!await userManager!.IsInRoleAsync(employee, "Avaliar_Funcionarios"))
            {
                await userManager!.AddToRoleAsync(employee, "Avaliar_Funcionarios");
            }
            if (!await userManager!.IsInRoleAsync(employee, "Role_Funcionario"))
            {
                await userManager!.AddToRoleAsync(employee, "Role_Funcionario");
            }
            if (!await userManager!.IsInRoleAsync(employee, "View_Reports"))
            {
                await userManager!.AddToRoleAsync(employee, "View_Reports");
            }
            if (!await userManager!.IsInRoleAsync(employee, "Create_Reports"))
            {
                await userManager!.AddToRoleAsync(employee, "Create_Reports");
            }


            var manager = await EnsureUserIsCreatedAsync(userManager!, "manager@ipg.pt", "Secret#123");
            if (!await userManager!.IsInRoleAsync(manager, "Avaliar_Funcionarios"))
            {
                await userManager!.AddToRoleAsync(manager, "Avaliar_Funcionarios");
            }
            if (!await userManager!.IsInRoleAsync(manager, "View_Reports"))
            {
                await userManager!.AddToRoleAsync(manager, "View_Reports");
            }
            if (!await userManager!.IsInRoleAsync(manager, "Create_Reports"))
            {
                await userManager!.AddToRoleAsync(manager, "Create_Reports");
            }
            if (!await userManager!.IsInRoleAsync(manager, "Create_Edit_Del_IssueType"))
            {
                await userManager!.AddToRoleAsync(manager, "Create_Edit_Del_IssueType");
            }
            if (!await userManager!.IsInRoleAsync(manager, "Edit_Del_Reports"))
            {
                await userManager!.AddToRoleAsync(manager, "Edit_Del_Reports");
            }

            var clienteAlberto = await EnsureUserIsCreatedAsync(userManager!, "zealberto@gmail.com", "Alberto#123");
            if (!await userManager!.IsInRoleAsync(clienteAlberto, ROLE_ADMIN3))
            {
                await userManager!.AddToRoleAsync(clienteAlberto, ROLE_ADMIN3);
            }

            var funcAndre = await EnsureUserIsCreatedAsync(userManager!, "andre@gmail.com", "Andre#123");
            if (!await userManager!.IsInRoleAsync(funcAndre, ROLE_ADMIN1))
            {
                await userManager!.AddToRoleAsync(funcAndre, ROLE_ADMIN1);
            }

            if (!await userManager!.IsInRoleAsync(manager, ROLE_MANAGER))
            {
                await userManager!.AddToRoleAsync(manager, ROLE_MANAGER);
            }
        }



        private static async Task<IdentityUser> EnsureUserIsCreatedAsync(UserManager<IdentityUser> userManager, string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user == null)
            {

                user = new IdentityUser(username);
                await userManager.CreateAsync(user, password);
            }

            return user;

        }

        internal static async System.Threading.Tasks.Task PopulateRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            await EnsureRoleIsCreatedAsync(roleManager!, ROLE_ADMIN);
            await EnsureRoleIsCreatedAsync(roleManager!, ROLE_CLIENT);
            await EnsureRoleIsCreatedAsync(roleManager!, "Avaliar_Funcionarios");
            await EnsureRoleIsCreatedAsync(roleManager!, "Role_Funcionario");
            await EnsureRoleIsCreatedAsync(roleManager!, ROLE_ADMIN3);
            await EnsureRoleIsCreatedAsync(roleManager!, ROLE_MANAGER);
            await EnsureRoleIsCreatedAsync(roleManager!, ROLE_EMPLOYEER);
            await EnsureRoleIsCreatedAsync(roleManager!, "Funcionário");
            await EnsureRoleIsCreatedAsync(roleManager!, "Gestor");
            await EnsureRoleIsCreatedAsync(roleManager!, "View_Reports");
            await EnsureRoleIsCreatedAsync(roleManager!, "Create_Edit_Del_IssueType");
            await EnsureRoleIsCreatedAsync(roleManager!, ROLE_REGISTER);
            await EnsureRoleIsCreatedAsync(roleManager!, "Create_Reports");
            await EnsureRoleIsCreatedAsync(roleManager!, "Edit_Del_Reports");

            await EnsureRoleIsCreatedAsync(roleManager!, ROLE_STOCK_ADMIN);
            await EnsureRoleIsCreatedAsync(roleManager!, ROLE_STOCK_OP);
        }




        private static async System.Threading.Tasks.Task EnsureRoleIsCreatedAsync(RoleManager<IdentityRole> roleManager, string name)
        {
            var role = await roleManager.FindByNameAsync(name);

            if (role == null)
            {
                role = new IdentityRole(name);
                await roleManager.CreateAsync(role);
            }
        }



        private static void PopulateType(SupermarketDbContext db)
        {
            if (db.IssueType.Any()) return;

            if (db.ProductExpiration.Any()) return;

            db.SaveChanges();
        }

        private static void PopulateIssueType(SupermarketDbContext db)
        {
            if (db.IssueType.Any()) return;

            db.IssueType.AddRange(
                new Models.IssueType { Name = "Cross-Contamination", IssueDescription = "Transfer of contaminants between products, often related to improper handling or storage" },
                new Models.IssueType { Name = "Customer Complaints", IssueDescription = "Issues reported by customers regarding the quality or safety of products" },
                new Models.IssueType { Name = "Deterioration and Damage", IssueDescription = "Products damaged during transportation, storage, or handling, compromising their quality" },
                new Models.IssueType { Name = "Inadequate Storage Conditions", IssueDescription = "Products stored in conditions that may affect their quality, such as temperature or humidity issues" },
                new Models.IssueType { Name = "Incorrect Pricing", IssueDescription = "Mismatch between the displayed price and the actual price at the point of sale" },
                new Models.IssueType { Name = "Mislabeling", IssueDescription = "Incorrect or misleading product labeling, including inaccurate nutritional information or allergen details" },
                new Models.IssueType { Name = "Out-of-Stock", IssueDescription = "Products unavailable for purchase despite being listed as in-stock" },
                new Models.IssueType { Name = "Sanitation and Cleanliness", IssueDescription = "Concerns related to the cleanliness and hygiene of the supermarket premises" }
              );

            db.SaveChanges();
        }

        private static void PopulatePurchase(SupermarketDbContext db)
        {
            if (db.Purchase.Any()) return;

            db.Purchase.AddRange(

                new Models.Purchase { ProductId = 1, SupplierId = 1, DeliveredQuantity = 30, DeliveryDate = new DateTime(2024, 01, 01, 00, 00, 00, 00), BatchNumber = "LOTE123", ExpirationDate = new DateTime(2024, 01, 05, 00, 00, 00, 00), EmployeeId = 1 },
                new Models.Purchase { ProductId = 2, SupplierId = 2, DeliveredQuantity = 50, DeliveryDate = new DateTime(2024, 01, 02, 00, 00, 00, 00), BatchNumber = "LOTE456", ExpirationDate = new DateTime(2024, 01, 06, 00, 00, 00, 00), EmployeeId = 2 },
                new Models.Purchase { ProductId = 3, SupplierId = 3, DeliveredQuantity = 70, DeliveryDate = new DateTime(2024, 01, 03, 00, 00, 00, 00), BatchNumber = "LOTE789", ExpirationDate = new DateTime(2024, 01, 07, 00, 00, 00, 00), EmployeeId = 3 }

              );

            db.SaveChanges();
        }

        private static void PopulateIssue(SupermarketDbContext db)
        {
            if (db.Issues.Any()) return;

            db.Issues.AddRange(

                new Models.Issues { ProductId = 1, IssueTypeId = 1, Description = "Issue Description 1", SupplierId = 1, EmployeeId = 1, Severity = Severity.Light },

                new Models.Issues { ProductId = 2, IssueTypeId = 3, Description = "Issue Description 2", SupplierId = 2, EmployeeId = 1, Severity = Severity.Light },
                new Models.Issues { ProductId = 1, IssueTypeId = 2, Description = "Issue Description 3", SupplierId = 1, EmployeeId = 4, Severity = Severity.Severe }
              );

            db.SaveChanges();
        }

        private static void PopulateOrder(SupermarketDbContext db)
        {
            if (db.Orders.Any()) return;

            db.Orders.AddRange(
                    new Orders
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Cream")!,
                        Quantity = 100,
                        Date = new DateTime(2023, 1, 1),
                        ClientCard = db.ClientCard.FirstOrDefault(a => a.ClientCardId == 1),
                    },
                    new Orders
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Sausages")!,
                        Quantity = 10,
                        Date = new DateTime(2023, 1, 1),
                        ClientCard = db.ClientCard.FirstOrDefault(a => a.ClientCardId == 1),
                    },
                    new Orders
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Sausages")!,
                        Quantity = 1000,
                        Date = new DateTime(2023, 5, 1),
                        ClientCard = db.ClientCard.FirstOrDefault(a => a.ClientCardId == 2),
                    },
                    new Orders
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Chips")!,
                        Quantity = 10000,
                        Date = new DateTime(2023, 9, 1),
                        ClientCard = db.ClientCard.FirstOrDefault(a => a.ClientCardId == 3),
                    },
                    new Orders
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Chips")!,
                        Quantity = 10000,
                        Date = new DateTime(2024, 1, 9),
                        ClientCard = db.ClientCard.FirstOrDefault(a => a.ClientCardId == 3),
                    },
                    new Orders
                    {
                        Product = db.Product.FirstOrDefault(a => a.Name == "Chips")!,
                        Quantity = 10000,
                        Date = new DateTime(2024, 1, 14),
                        ClientCard = db.ClientCard.FirstOrDefault(a => a.ClientCardId == 3),
                    }
                );

            db.SaveChanges();
        }
        private static void PopulatePonto(SupermarketDbContext db)
        {
            if (db.Ponto.Any()) return;

            db.Ponto.AddRange(
                new Ponto { CheckInTime = "07:30", CheckOutTime = "16:30", Date = DateTime.Now, LunchStartTime = "12:00", LunchEndTime = "13:00", RealCheckOutTime = "17:00", EmployeeId = 1, Status = "workOvertime", Justificative = "" },
                new Ponto { CheckInTime = "08:00", CheckOutTime = "17:00", Date = DateTime.Now, LunchStartTime = "12:30", LunchEndTime = "13:30", RealCheckOutTime = "18:00", EmployeeId = 2, Status = "workOvertime", Justificative = "" },
                new Ponto { CheckInTime = "07:45", CheckOutTime = "16:45", Date = DateTime.Now, LunchStartTime = "12:15", LunchEndTime = "13:15", RealCheckOutTime = "17:30", EmployeeId = 3, Status = "workOvertime", Justificative = "" },
                new Ponto { CheckInTime = "08:15", CheckOutTime = "17:15", Date = DateTime.Now, LunchStartTime = "12:45", LunchEndTime = "13:45", RealCheckOutTime = "18:15", EmployeeId = 4, Status = "workOvertime", Justificative = "" },
                new Ponto { CheckInTime = "07:30", CheckOutTime = "16:30", Date = DateTime.Now, LunchStartTime = "12:00", LunchEndTime = "13:00", RealCheckOutTime = "17:00", EmployeeId = 5, Status = "workOvertime", Justificative = "" },
                new Ponto { CheckInTime = "08:00", CheckOutTime = "17:00", Date = DateTime.Now, LunchStartTime = "12:30", LunchEndTime = "13:30", RealCheckOutTime = "18:00", EmployeeId = 6, Status = "workOvertime", Justificative = "" },
                new Ponto { CheckInTime = "07:45", CheckOutTime = "16:45", Date = DateTime.Now, LunchStartTime = "12:15", LunchEndTime = "13:15", RealCheckOutTime = "17:30", EmployeeId = 7, Status = "workOvertime", Justificative = "" },
                new Ponto { CheckInTime = "08:15", CheckOutTime = "17:15", Date = DateTime.Now, LunchStartTime = "12:45", LunchEndTime = "13:45", RealCheckOutTime = "18:15", EmployeeId = 8, Status = "workOvertime", Justificative = "" },
                new Ponto { CheckInTime = "07:30", CheckOutTime = "16:30", Date = DateTime.Now, LunchStartTime = "12:00", LunchEndTime = "13:00", RealCheckOutTime = "15:00", EmployeeId = 9, Status = "notworkOvertime", Justificative = "Médico" },
                new Ponto { CheckInTime = "08:00", CheckOutTime = "17:00", Date = DateTime.Now, LunchStartTime = "12:30", LunchEndTime = "13:30", RealCheckOutTime = "14:00", EmployeeId = 10, Status = "notworkOvertime", Justificative = "Médico" },
                new Ponto { CheckInTime = "07:45", CheckOutTime = "16:45", Date = DateTime.Now, LunchStartTime = "12:15", LunchEndTime = "13:15", RealCheckOutTime = "15:30", EmployeeId = 11, Status = "notworkOvertime", Justificative = "Médico" },
                new Ponto { CheckInTime = "08:15", CheckOutTime = "17:15", Date = DateTime.Now, LunchStartTime = "12:45", LunchEndTime = "13:45", RealCheckOutTime = "14:25", EmployeeId = 12, Status = "notworkOvertime", Justificative = "Médico" },
                new Ponto { CheckInTime = "07:30", CheckOutTime = "16:30", Date = DateTime.Now, LunchStartTime = "12:00", LunchEndTime = "13:00", RealCheckOutTime = "15:00", EmployeeId = 13, Status = "notworkOvertime", Justificative = "Médico" },
                new Ponto { CheckInTime = "08:00", CheckOutTime = "17:00", Date = DateTime.Now, LunchStartTime = "12:30", LunchEndTime = "13:30", RealCheckOutTime = "16:00", EmployeeId = 14, Status = "notworkOvertime", Justificative = "Médico" },
                new Ponto { CheckInTime = "07:45", CheckOutTime = "16:45", Date = DateTime.Now, LunchStartTime = "12:15", LunchEndTime = "13:15", RealCheckOutTime = "15:30", EmployeeId = 15, Status = "notworkOvertime", Justificative = "Médico" }
            );

            db.SaveChanges();
        }


    }
}