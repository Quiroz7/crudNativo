using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallerNativo.Models;

namespace TallerNativo.Controllers
{
    public class ProductoesController : Controller
    {
        private readonly TallerCrudContext _context;

        public ProductoesController(TallerCrudContext context)
        {
            _context = context;
        }

        // GET: Productoes
        public async Task<IActionResult> Index()
        {
              return _context.Productos != null ? 
                          View(await _context.Productos.ToListAsync()) :
                          Problem("Entity set 'TallerCrudContext.Productos'  is null.");
        }

        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProductos == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProductos,Codigo,Nombre,Precio,Cantidad")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                Producto productoEnDB = _context.Productos.SingleOrDefault(c => c.Codigo == producto.Codigo);
                if (productoEnDB != null)
                {
                    ModelState.AddModelError("codigo", "ya está registrado este código");
                    return View(producto);
                }

                    _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProductos,Codigo,Nombre,Precio,Cantidad")] Producto producto)
        {
            if (id != producto.IdProductos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProductos))
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
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProductos == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'TallerCrudContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
          return (_context.Productos?.Any(e => e.IdProductos == id)).GetValueOrDefault();
        }

        public IActionResult Search(string? searchCodigo)
        {
            var productos = _context.Productos.AsQueryable();

            if (!string.IsNullOrEmpty(searchCodigo) && int.TryParse(searchCodigo, out int codigoValue))
            {
                productos = productos.Where(l => l.Codigo == codigoValue);
            }

            return View(productos.ToList());
        }


    }
}
