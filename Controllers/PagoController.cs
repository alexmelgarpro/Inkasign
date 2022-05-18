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

        [HttpPost]

        public IActionResult Pagar(Pago pago){

            return null;
        }

        
    }
}