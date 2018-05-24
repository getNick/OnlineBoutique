using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MarketCore.Classes;
using MarketCore.EmunsAndConst;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Microsoft.Net.Http.Headers;
using OnlineBoutique.Data;
using Size = MarketCore.Classes.Size;

namespace OnlineBoutique.Controllers
{
    public class CatalogController : Controller
    {
        BoutiqueContex db;
        private IHostingEnvironment _hostingEnvironment;
        private Random rnd;

        public CatalogController(BoutiqueContex context, IHostingEnvironment environment)
        {
            db = context;
            _hostingEnvironment = environment;
            rnd = new Random();
            //foreach (var prod in db.Products)
            //{
            //    db.Products.Remove(prod);
            //}
            //foreach (var productVariation in db.ProductVariations)
            //{
            //    db.ProductVariations.Remove(productVariation);
            //}

            //db.SaveChanges();
        }
        public IActionResult Index()
        {
            var list = db.Products.ToList();
            return View(list);
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

        public IActionResult CreateNewProduct()
        {
            var product = new Product();
            TempProductStore.getInstance().Products.Add(product);
            return View(product.Id);
        }

        public IActionResult NewProduct(int? productId = null, string name=null,string description=null)
        {
            var product = TempProductStore.getInstance().Products.FirstOrDefault(x => x.Id == productId);
            product.Name = name;
            product.Description = description;
            db.Products.Add(product);
            db.SaveChanges();
            TempProductStore.getInstance().Products.Remove(product);
            return View("Index", db.Products.ToList());
        }

        public IActionResult ProductVariation(int productVariationID)
        {
            var pv = db.ProductVariations.Find(productVariationID);
            var product = db.Products.Find(pv.BaseProduct.Id);
            product.ProductVariations = db.ProductVariations.Where(x => x.BaseProduct.Id == product.Id).ToList();
            pv.BaseProduct = product;
            return View(pv);
        }

        public IActionResult CreateProductVariation(int? baseProductId=null)
        {
            var productVariation = new ProductVariation();
            productVariation.BaseProduct = db.Products.Find(baseProductId);
            TempProductStore.getInstance().ProductVariations.Add(productVariation);
            return View(productVariation);
        }

        public IActionResult NewProductVariation(int? productVariationId = null,string color=null,
            int? namedSize=null,double? breastSize=null, double? girthHandsSize = null,
            double? girthOfThighsSize = null,double? height = null,double? hipsSize = null,
            double? waistSize = null,double? widthOfTheShoulders = null,double? price = null,int? countInStore=null)
        {

            var pv = TempProductStore.getInstance().ProductVariations.FirstOrDefault(x => x.Id == productVariationId);
            pv.ColorVariation.Color = color;
            pv.SizeVariation = new SizeVariation()
            {
                NamedSize = (NamedSizeEnum) namedSize,
            };
            pv.SizeVariation.ListParams = new List<Size>();
            pv.SizeVariation.ListParams.Add(new Size(){TypeSize = SizesEnum.Breast,Value = (double)breastSize});
            pv.SizeVariation.ListParams.Add(new Size() { TypeSize = SizesEnum.Height, Value = (double)height });
            pv.SizeVariation.ListParams.Add(new Size() { TypeSize = SizesEnum.GirthHands, Value = (double)girthHandsSize });
            pv.SizeVariation.ListParams.Add(new Size() { TypeSize = SizesEnum.GirthOfThighs, Value = (double)girthOfThighsSize });
            pv.SizeVariation.ListParams.Add(new Size() { TypeSize = SizesEnum.Hips, Value = (double)hipsSize });
            pv.SizeVariation.ListParams.Add(new Size() { TypeSize = SizesEnum.Waist, Value = (double)waistSize });
            pv.SizeVariation.ListParams.Add(new Size() { TypeSize = SizesEnum.WidthOfTheShoulders, Value = (double)widthOfTheShoulders });
            pv.Price = (double)price;
            pv.CountInStore = (int) countInStore;

            return View("Index");
        }
        [HttpPost]
        public IActionResult Upload(int? productId= null,int? productVariationId = null)
        {
            var images=new List<FilePath>();
            var files = HttpContext.Request.Form.Files;
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, @"images\products");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var name = rnd.Next(0, int.MaxValue)+".jpg";
                    var filePath = Path.Combine(uploads, name);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    images.Add(new FilePath(name));

                }
            }
            if (productId != null)
            {
                var product = TempProductStore.getInstance().Products.FirstOrDefault(x => x.Id == productId);
                product.ImageUrl = images.FirstOrDefault();
            }
            else if(productVariationId != null)
            {
                var pv = TempProductStore.getInstance().ProductVariations.FirstOrDefault(x => x.Id == productVariationId);

                if (pv.ColorVariation == null)
                {
                    pv.ColorVariation=new ColorVariation();
                    pv.ColorVariation.ImageURLs = images;
                }
                else
                {
                    pv.ColorVariation.ImageURLs.AddRange(images);
                }
            }
            return Ok();
        }
    }
}