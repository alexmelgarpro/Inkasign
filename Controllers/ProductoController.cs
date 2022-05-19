using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inkasign.Data;
using Inkasign.Models;
using Microsoft.AspNetCore.Authorization;


namespace Inkasign.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ProductoController : Controller
    {
       
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {  
            _context = context;
            
        }

        public async Task<IActionResult> ListarProducto()
        {
            return View(await _context.DataProductos.ToListAsync());
        }

        public async Task<IActionResult> DetalleProducto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.DataProductos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        public IActionResult RegistrarProducto()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarProducto([Bind("Id,Nombre,Descripcion,Precio,Imagen")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListarProducto));
            }
            return View(producto);
        }

        public IActionResult ElegirAccion(){
            return View();
        }

        public async Task<IActionResult> EditarProducto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.DataProductos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarProducto(int id, [Bind("Id,Nombre,Descripcion,Precio,Imagen")] Producto producto)
        {
            if (id != producto.Id)
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
                    if (!ProductExists(producto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListarProducto));
            }
            return View(producto);
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            
            var detalles = _context.DataDetallePedido.Where(d => d.Producto.Id == id);
            
            foreach(var item in detalles){
                item.Producto = null;
            }

            var proformas = _context.DataProforma.Where(p => p.Producto.Id == id);

            foreach(var item in proformas){
                _context.DataProforma.Remove(item);
            }

            var product = await _context.DataProductos.FindAsync(id);
            _context.DataProductos.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListarProducto));
        }

        private bool ProductExists(int id)
        {
            return _context.DataProductos.Any(e => e.Id == id);
        }

     

    }
}