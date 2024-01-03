﻿namespace Supermarket.Models
{
    public class ProductPurchaseSupplier
    {
        public int ProductPurchaseSupplierId { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int PurchaseSupplierId { get; set; }
        public PurchaseSupplier? PurchaseSupplier { get; set; }
        public int AskedQuantity { get; set; }
        public int DeliveredQuantity { get; set; }

        public DateTime EstimateDeliveryTime { get; set; }

        public DateTime DeliveredDate { get; set; }

        public int LineTotal { get; set; }

    }
}
