using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBoutique.Data;
using OnlineBoutique.Models;

namespace OnlineBoutique.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;
        public HomeController(ApplicationDbContext contex)
        {
            db = contex;
        }
        public IActionResult Index()
        {
            //RedirectToAction("CreateNewProduct", "Catalog");
            
            var list = db.ProductVariations.Include(x => x.ColorVariation).ThenInclude(x => x.ImageURLs)
                .Include(x => x.BaseProduct).ToList();
            foreach (var pv in list)
            {
                if (pv.ColorVariation == null)
                {
                    db.ProductVariations.Remove(pv);
                }
            }
            db.SaveChanges();
            list = db.ProductVariations.Include(x => x.ColorVariation).ThenInclude(x => x.ImageURLs)
                .Include(x => x.BaseProduct).ToList();
            
            return View(list);

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
