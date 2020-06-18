﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EarthquakeHorses4.Data;
using EarthquakeHorses4.Models;

namespace EarthquakeHorses4.Controllers
{
    public class MaterielsController : Controller
    {
        private readonly ApplicationContext _context;

        public MaterielsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Materiels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Materiel.ToListAsync());
        }

        // GET: Materiels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiel = await _context.Materiel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materiel == null)
            {
                return NotFound();
            }

            return View(materiel);
        }

        // GET: Materiels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Materiels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Reference,Tarif,Type,Taille,Unite,Couleur,UserId")] Materiel materiel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materiel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(materiel);
        }

        // GET: Materiels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiel = await _context.Materiel.FindAsync(id);
            if (materiel == null)
            {
                return NotFound();
            }
            return View(materiel);
        }

        // POST: Materiels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Reference,Tarif,Type,Taille,Unite,Couleur,UserId")] Materiel materiel)
        {
            if (id != materiel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materiel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterielExists(materiel.Id))
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
            return View(materiel);
        }

        // GET: Materiels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiel = await _context.Materiel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materiel == null)
            {
                return NotFound();
            }

            return View(materiel);
        }

        // POST: Materiels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materiel = await _context.Materiel.FindAsync(id);
            _context.Materiel.Remove(materiel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterielExists(int id)
        {
            return _context.Materiel.Any(e => e.Id == id);
        }
    }
}
