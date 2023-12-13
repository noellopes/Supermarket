﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class IssueTypesController : Controller
    {
        private readonly SupermarketDbContext _context;

        public IssueTypesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: IssueTypes
        public async Task<IActionResult> Index()
        {
              return _context.IssueType != null ? 
                          View(await _context.IssueType.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.IssueType'  is null.");
        }

        // GET: IssueTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IssueType == null)
            {
                return NotFound();
            }

            var issueType = await _context.IssueType
                .FirstOrDefaultAsync(m => m.IssueTypeId == id);
            if (issueType == null)
            {
                return NotFound();
            }

            return View(issueType);
        }

        // GET: IssueTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IssueTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IssueTypeId,Name,IssueDescription")] IssueType issueType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issueType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(issueType);
        }

        // GET: IssueTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IssueType == null)
            {
                return NotFound();
            }

            var issueType = await _context.IssueType.FindAsync(id);
            if (issueType == null)
            {
                return NotFound();
            }
            return View(issueType);
        }

        // POST: IssueTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IssueTypeId,Name,IssueDescription")] IssueType issueType)
        {
            if (id != issueType.IssueTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issueType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueTypeExists(issueType.IssueTypeId))
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
            return View(issueType);
        }

        // GET: IssueTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IssueType == null)
            {
                return NotFound();
            }

            var issueType = await _context.IssueType
                .FirstOrDefaultAsync(m => m.IssueTypeId == id);
            if (issueType == null)
            {
                return NotFound();
            }

            return View(issueType);
        }

        // POST: IssueTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IssueType == null)
            {
                return Problem("Entity set 'SupermarketDbContext.IssueType'  is null.");
            }
            var issueType = await _context.IssueType.FindAsync(id);
            if (issueType != null)
            {
                _context.IssueType.Remove(issueType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueTypeExists(int id)
        {
          return (_context.IssueType?.Any(e => e.IssueTypeId == id)).GetValueOrDefault();
        }
    }
}