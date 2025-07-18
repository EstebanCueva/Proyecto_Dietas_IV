﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class DietasController : Controller
    {
        private readonly ProyectoContext _context;

        public DietasController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: Dietas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dieta.ToListAsync());
        }

        // GET: Dietas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dieta = await _context.Dieta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dieta == null)
            {
                return NotFound();
            }

            return View(dieta);
        }

        // GET: Dietas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dietas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameDiet,DescriptionDiet,Calories,Proteins,Carbohydrates,DietType")] Dieta dieta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dieta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dieta);
        }

        // GET: Dietas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dieta = await _context.Dieta.FindAsync(id);
            if (dieta == null)
            {
                return NotFound();
            }
            return View(dieta);
        }

        // POST: Dietas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameDiet,DescriptionDiet,Calories,Proteins,Carbohydrates,DietType")] Dieta dieta)
        {
            if (id != dieta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dieta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DietaExists(dieta.Id))
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
            return View(dieta);
        }

        // GET: Dietas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dieta = await _context.Dieta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dieta == null)
            {
                return NotFound();
            }

            return View(dieta);
        }

        // POST: Dietas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dieta = await _context.Dieta.FindAsync(id);
            if (dieta != null)
            {
                _context.Dieta.Remove(dieta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DietaExists(int id)
        {
            return _context.Dieta.Any(e => e.Id == id);
        }

        // POST: Mostrar dieta según calorías del usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MostrarDietaSegunCalorias(string idUsuario)
        {
            var usuario = await _context.Usuario.FindAsync(idUsuario);

            if (usuario == null)
            {
                return NotFound();
            }

            int calorias = usuario.TotalCalories;

            if (calorias < 2000)
            {
                return RedirectToAction("DietaBaja", new { id = usuario.Id });
            }
            else if (calorias < 3000)
            {
                return RedirectToAction("DietaMedia", new { id = usuario.Id });

            }
            else
            {
                return RedirectToAction("DietaAlta", new { id = usuario.Id });
            }
        }

        // Vistas de dietas personalizadas según calorías
        public async Task<IActionResult> DietaBaja(string id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null) return NotFound();
            return View("DietaBaja", usuario);
        }

        public async Task<IActionResult> DietaMedia(string id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null) return NotFound();
            return View("DietaMedia", usuario);
        }

        public async Task<IActionResult> DietaAlta(string id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null) return NotFound();
            return View("DietaAlta", usuario);
        }
    }
}
