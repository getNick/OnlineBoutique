using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using MarketCore.Classes;
using MarketCore.EmunsAndConst;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using OnlineBoutique.Data;

namespace OnlineBoutique.Controllers
{
    public class CatalogController : Controller
    {
        BoutiqueContex db;
        public CatalogController(BoutiqueContex context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public IActionResult Param(GenderCategoriesEnum gender,SeasonCategoriesEnum season,DressTypeEnum dressType,int? year=null,double? from=null,double? to=null)
        {
            var products = gender == GenderCategoriesEnum.All ? db.Products : db.Products.Where(x => x.Category.Gender == gender);
            products = season == SeasonCategoriesEnum.All ? products:products.Where(x => x.Category.Season == season);
            products = dressType == DressTypeEnum.All ? products : products.Where(x => x.Category.DressType == dressType);
            products = year == null ? products : products.Where(x => x.Category.Year == year);
            products = from == null ? products : products.Where(x => x.Price> from);
            products = to == null ? products : products.Where(x => x.Price < to);
            return View("Index",products.ToList());
        }
    }
}