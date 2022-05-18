using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Inkasign.Data;
using Inkasign.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;

namespace Inkasign.Controllers
{
    public class PagoController : Controller
    {
       
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PagoController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {      

            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(Decimal monto)
        {
            Pago pago = new Pago();
            pago.UserID = _userManager.GetUserName(User);
            pago.MontoTotal = monto;
            return View(pago);
        }
        public IActionResult Confirmacion(){

            return View();
        }

        [HttpPost]

        public IActionResult Pagar(Pago pago){
            
            pago.PaymentDate = DateTime.UtcNow;
            pago.UserID = _userManager.GetUserName(User);
            _context.Add(pago);

            var itemsProforma = from o in _context.DataProforma select o;
            itemsProforma = itemsProforma.
                Include(p => p.Producto).
                Where(p => p.UserID.Equals(pago.UserID) && p.Status.Equals("Pendiente"));
             
                    
            Pedido pedido = new Pedido();
            pedido.UserID = pago.UserID;
            pedido.Total = pago.MontoTotal;
            pedido.Pago = pago;
           
            _context.Add(pedido);

            List<DetallePedido> itemsPedido = new List<DetallePedido>();
            foreach(var item in itemsProforma.ToList()){
                DetallePedido detallePedido = new DetallePedido();
                detallePedido.Pedido=pedido;
                detallePedido.Precio = item.Precio;
                detallePedido.Producto = item.Producto;
                detallePedido.Cantidad = item.Cantidad;
                itemsPedido.Add(detallePedido);
            }

            _context.AddRange(itemsPedido);

            foreach (Proforma p in itemsProforma.ToList())
            {
                p.Status="Procesado";
            }
            _context.UpdateRange(itemsProforma);

            _context.SaveChanges();

            ViewData["Message"] = "¡PAGO REALIZADO!";
                     
            return RedirectToAction("Confirmacion");


        }

        
    }
}