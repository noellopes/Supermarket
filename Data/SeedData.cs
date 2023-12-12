﻿using Supermarket.Models;

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
            PopulateWarehouseSection_Product(db);
            PopulateReduceProduct(db);
        }

        private static void PopulateBrand(SupermarketDbContext db)
        {
            if (db.Brand.Any()) return;

            db.Brand.AddRange(
                    new Brand { Name = "Nivea" },
                    new Brand { Name = "Frankfurt" },
                    new Brand { Name = "Lays" }
                );

            db.SaveChanges();
        }

        private static void PopulateCategory(SupermarketDbContext db)
        {
            if (db.Category.Any()) return;

            db.Category.AddRange(
                    new Category { Name = "Hygiene" },
                    new Category { Name = "Canned" },
                    new Category { Name = "Drinks" }
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
    }
}