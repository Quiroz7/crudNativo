﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallerNativo.Models;

namespace Tienda.Controllers
{
    public class VentasController : Controller
    {
        private readonly TallerCrudContext _context;

        public VentasController(TallerCrudContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index(int option, string filter)
        {
            var TallerCrudContext = _context.Ventas.Include(v => v.Cliente);
            if (!String.IsNullOrEmpty(filter))
                return View(await _context.Ventas.Include(v => v.Cliente)
                    .Where(x => option == 1 ? x.Cliente.Cedula == Convert.ToInt32(filter) : x.IdVenta == Convert.ToInt32(filter)).ToListAsync());

            return View(await TallerCrudContext.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (venta == null)
            {
                return NotFound();
            }
            venta.DetallesVenta = await _context.DetallesVentas
                .Where(x => x.VentaId == venta.IdVenta)
                .Include(v => v.Producto)
                .ToListAsync();
            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            var cosa = new SelectList(_context.Clientes, "IdClientes", "Cedula", "Nombre");
            ViewData["IdClientes"] = cosa;
            return View();
        }

        // POST: Ventas/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVenta,FechaVenta,IdClientes,Total")] Venta venta)
        {
            Console.WriteLine($"Ingresa al crear venta {!ModelState.IsValid}");
            if (!ModelState.IsValid)
            {
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdClientes"] = new SelectList(_context.Clientes, "IdClientes", "IdClientes", venta.IdClientes);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            ViewData["IdClientes"] = new SelectList(_context.Clientes, "IdClientes", "IdClientes", venta.IdClientes);
            return View(venta);
        }

        // POST: Ventas/Edit/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVenta,FechaVenta,IdClientes,Total")] Venta venta)
        {
            if (id != venta.IdVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.IdVenta))
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
            ViewData["IdClientes"] = new SelectList(_context.Clientes, "IdClientes", "IdClientes", venta.IdClientes);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ventas == null)
            {
                return Problem("Entity set 'TallerCrudContext.Ventas'  is null.");
            }
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return (_context.Ventas?.Any(e => e.IdVenta == id)).GetValueOrDefault();
        }

       
    }
}
