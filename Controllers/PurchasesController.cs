﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly SupermarketDbContext _context;

        public PurchasesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Purchases
        [Authorize(Roles = "View_Reports")]
        public async Task<IActionResult> Index(int page = 1, string product = "", string supplier = "", DateTime? deliverydate = null)
        {
            // Call the method to update expiration status
            //UpdateExpirationStatusForAllPurchases();

            var purchase = from i in _context.Purchase.Include(p => p.Product)                                      
                                                      .Include(s => s.Supplier)  
                                                      .Include(e => e.Employee)
                         select i;
            if (product != "")
            {
                purchase = purchase.Where(x => x.Product!.Name.Contains(product));
            }
                        
            if (supplier != "")
            {
                purchase = purchase.Where(x => x.Supplier!.Name.Contains(supplier));
            }

            if (deliverydate.HasValue)
            {
                purchase = purchase.Where(x => x.DeliveryDate == deliverydate.Value.Date);
            }


            //if (deliverydate != "")
            //{
            //    purchase = purchase.Where(x => x.DeliveryDate!.Contains(deliverydate));
            //}


            var pagination = new PagingInfo
            {
                CurrentPage = page,
                PageSize = PagingInfo.DEFAULT_PAGE_SIZE,
                TotalItems = purchase.Count()
            };

            return View(
                new PurchaseListViewModel
                {
                    Purchase = purchase.OrderByDescending(i => i.DeliveryDate)
                                       .Skip((page - 1) * pagination.PageSize)
                                                 .Take(pagination.PageSize),
                    Pagination = pagination,
                    SearchProduct = product,
                    SearchSupplier = supplier,
                    SearchDeliveryDate = deliverydate
                }
            );
        }

        // GET: Purchases/Details/5
        [Authorize(Roles = "View_Reports")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Purchase == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase
                .Include(p => p.Product)
                .Include(p => p.Supplier)
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.PurchaseId == id);
            if (purchase == null)
            {
                return View("PurchaseInexistent");
            }

            return View(purchase);
        }

        // GET: Purchases/Create
        [Authorize(Roles = "Create_Reports")]        
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name");
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create_Reports")]
        public async Task<IActionResult> Create([Bind("PurchaseId,ProductId,SupplierId,EmployeeId,DeliveredQuantity,DeliveryDate,BatchNumber,ExpirationDate,ProductExpired")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                // Set ProductExpired based on expiration date
                purchase.ProductExpired = IsProductExpired(purchase.ExpirationDate);

                _context.Add(purchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", purchase.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name", purchase.SupplierId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name");
            return View(purchase);
        }

        // GET: Purchases/Edit/5        
        [Authorize(Roles = "Edit_Del_Reports")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Purchase == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase.FindAsync(id);
            if (purchase == null)
            {
                return View("PurchaseInexistent");
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", purchase.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name", purchase.SupplierId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name");
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit_Del_Reports")]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseId,ProductId,SupplierId,EmployeeId,DeliveredQuantity,DeliveryDate,BatchNumber,ExpirationDate,ProductExpired")] Purchase purchase)
        {
            if (id != purchase.PurchaseId)
            {
                return NotFound();
            }

            // Set ProductExpired based on expiration date
            purchase.ProductExpired = IsProductExpired(purchase.ExpirationDate);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();

                    // Encontra a purchase original
                    // Update information in ExpiredProducts
                    var expiredProduct = await _context.ExpiredProducts
                        .FirstOrDefaultAsync(ep => ep.ProductId == purchase.ProductId);

                    // Para checar se o produto não está mais expirado
                    if (expiredProduct != null)
                    {
                        if (purchase.ProductExpired)
                        {
                            expiredProduct.ProductId = purchase.ProductId;
                            expiredProduct.ExpirationDate = purchase.ExpirationDate;
                            expiredProduct.BatchNumber = purchase.BatchNumber;

                            _context.Update(expiredProduct);
                        }
                        else
                        {
                            // If the product is no longer expired, remove it from ExpiredProducts
                            _context.ExpiredProducts.Remove(expiredProduct);
                        }

                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.PurchaseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", purchase.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name", purchase.SupplierId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name");
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        [Authorize(Roles = "Edit_Del_Reports")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Purchase == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase
                .Include(p => p.Product)
                .Include(p => p.Supplier)
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.PurchaseId == id);
            if (purchase == null)
            {
                return View("PurchaseInexistent");
            }

            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit_Del_Reports")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Purchase == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Purchase'  is null.");
            }

            var purchase = await _context.Purchase
                .Include(p => p.Product)
                .Include(p => p.Supplier)
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.PurchaseId == id);

            if (purchase != null)
            {
                _context.Purchase.Remove(purchase);
            }

            await _context.SaveChangesAsync();
            return View("PurchaseDeleted", purchase);
        }

        private bool PurchaseExists(int id)
        {
          return (_context.Purchase?.Any(e => e.PurchaseId == id)).GetValueOrDefault();
        }


        public bool IsProductExpired(DateTime ExpirationDate)
        {
            return DateTime.Now > ExpirationDate;
        }

        // Verifica se os produtos estão expirados
        //internal void UpdateExpirationStatusForAllPurchases()
        //{
        //    var allPurchases = _context.Purchase;

        //    foreach (var purchase1 in allPurchases)
        //    {
        //        if (IsProductExpired(purchase1.ExpirationDate))
        //        {
        //            // Update the expiration status for the product in the purchase1
        //            purchase1.ProductExpired = true;

        //            // Checa se o produto está expirado e ainda não está na tabela de ExpiredProducts
        //            if (purchase1.ProductExpired &&
        //                !_context.ExpiredProducts.Any(ep => ep.ProductId == purchase1.ProductId))
        //            {

        //                //Escrever na tabela de produtos expirados
        //                var productExpired = new ProductExpiration
        //                {
        //                    ProductId = purchase1.ProductId,
        //                    BatchNumber = purchase1.BatchNumber,
        //                    ExpirationDate = purchase1.ExpirationDate,
        //                    Quantity = purchase1.DeliveredQuantity
        //                };

        //                _context.ExpiredProducts.Add(productExpired);
        //            }
        //        }
        //        else
        //        {
        //            purchase1.ProductExpired = false;
        //        }
        //    }

        //    _context.SaveChanges();
        //}

        [Authorize(Roles = "View_Reports")]
        public IActionResult CloseToExpire(int page = 1, string product = "", string supplier = "", DateTime? expirationdate = null)
        {
            var currentDate = DateTime.Now;
            var NextExpirationDate = currentDate.AddDays(7); // 1 week from now

            var purchase = from i in _context.Purchase
                            .Include(p => p.Product)
                            .Include(s => s.Supplier)
                            .Include(e => e.Employee)
                            .Where(x => x.ExpirationDate > currentDate && x.ExpirationDate <= NextExpirationDate)
                            select i;

            if (product != "")
            {
                purchase = purchase.Where(x => x.Product!.Name.Contains(product));
            }

            if (supplier != "")
            {
                purchase = purchase.Where(x => x.Supplier!.Name.Contains(supplier));
            }

            if (expirationdate.HasValue) purchase = purchase.Where(x => x.ExpirationDate == expirationdate);


            var pagination = new PagingInfo
            {
                CurrentPage = page,
                PageSize = PagingInfo.DEFAULT_PAGE_SIZE,
                TotalItems = purchase.Count()
            };

            return View(
                new PurchaseListViewModel
                {
                    Purchase = purchase.OrderBy(i => i.ExpirationDate)
                                       .Skip((page - 1) * pagination.PageSize)
                                                 .Take(pagination.PageSize),
                    Pagination = pagination,
                    SearchProduct = product,
                    SearchSupplier = supplier,
                    SearchExpirationDate = expirationdate
                }
            );
        }

        // É chamado quando a aplicação inicia
        public IActionResult OnApplicationStart()
        {
            // Chama o método para atualizar o status de expiração para todas as compras
            //UpdateExpirationStatusForAllPurchases();

            // Redireciona para a ação "Index" do controlador atual
            return RedirectToAction(nameof(Index));
        }
    }
}