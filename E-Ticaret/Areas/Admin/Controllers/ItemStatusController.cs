﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Ticaret.Models;

namespace E_Ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemStatusController : Controller
    {
        private readonly eTicaretContext _context;

        public ItemStatusController(eTicaretContext context)
        {
            _context = context;
        }

        // GET: Admin/ItemStatus
        public async Task<IActionResult> Index()
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null) //adminId'si yoksa hata ver.
            {
                return RedirectToAction("Index", "Home");
            }
            return _context.ItemStatus != null ? 
                          View(await _context.ItemStatus.ToListAsync()) :
                          Problem("Entity set 'eTicaretContext.ItemStatus'  is null.");
        }

        // GET: Admin/ItemStatus/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null) //adminId'si yoksa hata ver.
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.ItemStatus == null)
            {
                return NotFound();
            }

            var itemStatus = await _context.ItemStatus
                .FirstOrDefaultAsync(m => m.ItemStatusId == id);
            if (itemStatus == null)
            {
                return NotFound();
            }

            return View(itemStatus);
        }

        // GET: Admin/ItemStatus/Create
        public IActionResult Create()
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null) //adminId'si yoksa hata ver.
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Admin/ItemStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemStatusId,ItemStatusName")] ItemStatus itemStatus)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null) //adminId'si yoksa hata ver.
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(itemStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemStatus);
        }

        // GET: Admin/ItemStatus/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null) //adminId'si yoksa hata ver.
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.ItemStatus == null)
            {
                return NotFound();
            }

            var itemStatus = await _context.ItemStatus.FindAsync(id);
            if (itemStatus == null)
            {
                return NotFound();
            }
            return View(itemStatus);
        }

        // POST: Admin/ItemStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("ItemStatusId,ItemStatusName")] ItemStatus itemStatus)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null) //adminId'si yoksa hata ver.
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != itemStatus.ItemStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemStatusExists(itemStatus.ItemStatusId))
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
            return View(itemStatus);
        }

        // GET: Admin/ItemStatus/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null) //adminId'si yoksa hata ver.
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.ItemStatus == null)
            {
                return NotFound();
            }

            var itemStatus = await _context.ItemStatus
                .FirstOrDefaultAsync(m => m.ItemStatusId == id);
            if (itemStatus == null)
            {
                return NotFound();
            }

            return View(itemStatus);
        }

        // POST: Admin/ItemStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null) //adminId'si yoksa hata ver.
            {
                return RedirectToAction("Index", "Home");
            }
            if (_context.ItemStatus == null)
            {
                return Problem("Entity set 'eTicaretContext.ItemStatus'  is null.");
            }
            var itemStatus = await _context.ItemStatus.FindAsync(id);
            if (itemStatus != null)
            {
                _context.ItemStatus.Remove(itemStatus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemStatusExists(short id)
        {
          return (_context.ItemStatus?.Any(e => e.ItemStatusId == id)).GetValueOrDefault();
        }
    }
}