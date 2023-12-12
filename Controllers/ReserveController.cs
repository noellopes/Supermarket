﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supermarket.Data;

namespace Supermarket.Controllers
{
    public class ReserveController : Controller
    {
        private readonly SupermarketDbContext _context;

        public ReserveController(SupermarketDbContext context)
        {
            _context = context;
        }


        // GET: ReserveController
        public async Task <ActionResult> Index()
        {
            return View();
        }

        // GET: ReserveController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReserveController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReserveController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReserveController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReserveController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReserveController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReserveController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}