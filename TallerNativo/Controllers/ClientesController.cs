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
    public class ClientesController : Controller
    {
        private readonly TallerCrudContext _context;

        public ClientesController(TallerCrudContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
              return _context.Clientes != null ? 
                          View(await _context.Clientes.ToListAsync()) :
                          Problem("Entity set 'TallerCrudContext.Clientes'  is null.");
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdClientes == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdClientes,Cedula,Nombre,Apellido,Direccion,Telefono")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                //Busca la cédula en la base de datos
                Cliente clienteEnDB = _context.Clientes.SingleOrDefault(c => c.Cedula == cliente.Cedula);

                    if (clienteEnDB != null)
                        {
                            ModelState.AddModelError("cedula", "ya está registrada esta cédula");
                            return View(cliente);
                        }

            
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdClientes,Cedula,Nombre,Apellido,Direccion,Telefono")] Cliente cliente)
        {
            if (id != cliente.IdClientes)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdClientes))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdClientes == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'TallerCrudContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Clientes?.Any(e => e.IdClientes == id)).GetValueOrDefault();
        }

        public IActionResult Search(string? searchCedula)
        {
            var clientes = _context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(searchCedula) && int.TryParse(searchCedula, out int cedulaValue))
            {
                clientes = clientes.Where(l => l.Cedula == cedulaValue);
            }

            return View(clientes.ToList());
        }

        

    }    
}
