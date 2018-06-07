using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MarketCore.Classes;
using MarketCore.EmunsAndConst;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Microsoft.Net.Http.Headers;
using OnlineBoutique.Data;
using OnlineBoutique.Models;
using OnlineBoutique.Models.CatalogViewModels;
using OnlineBoutique.Models.EmunsAndConst;
using Size = MarketCore.Classes.Size;

namespace OnlineBoutique.Controllers
{
    public class CatalogController : Controller
    {
        ApplicationDbContext db;
        private IHostingEnvironment _hostingEnvironment;
        private Random rnd;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CatalogController(ApplicationDbContext context, IHostingEnvironment environment, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            db = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _hostingEnvironment = environment;
            rnd = new Random();
        }
        public IActionResult Index()
        {
            var list = db.Products.Include(x=>x.Category).ToList();
            var productVariations = db.ProductVariations.Include(x => x.ColorVariation).ThenInclude(x => x.ImageURLs).ToList();
           
            return View(productVariations);
        }

        public IActionResult Param(GenderCategoriesEnum gender,SeasonCategoriesEnum season,DressTypeEnum dressType,int? year=null,double? from=null,double? to=null)
        {
            var products = gender == GenderCategoriesEnum.All ? db.Products : db.Products.Where(x => x.Category.Gender == gender);
            //products = season == SeasonCategoriesEnum.All ? products:products.Where(x => x.Category.Season == season);
            products = dressType == DressTypeEnum.All ? products : products.Where(x => x.Category.DressType == dressType);
            products = year == null ? products : products.Where(x => x.Category.Year == year);
            //products = from == null ? products : products.Where(x => x.Price> from);
            //products = to == null ? products : products.Where(x => x.Price < to);
            return View("Index",products.ToList());
        }
        [HttpGet]
        public IActionResult CreateNewProduct()
        {
            return View();
        }

        public IActionResult NewProduct(string name=null,string brand=null,string description=null,string gender=null, string style =null, string dressType =null,int? year=null)
        {
            var product = new Product();
            product.Name = name;
            product.Description = description;
            product.Brand = brand;
            product.Category = new Category()
            {
                DressType = (DressTypeEnum)Enum.Parse(typeof(DressTypeEnum), dressType, true),
                Gender = (GenderCategoriesEnum)Enum.Parse(typeof(GenderCategoriesEnum), gender, true),
                Style = (StyleEnum)Enum.Parse(typeof(StyleEnum), style, true),
                Year = year,
            };
            db.Products.Add(product);
            db.SaveChanges();
            switch (product.Category.DressType)
            {
                case DressTypeEnum.DressAndSkirt: { return RedirectToAction("CreateSkirt", new { baseProductId = product.ProductId }); }break;
                case DressTypeEnum.Tshirt: { return RedirectToAction("CreateTshirt", new { baseProductId = product.ProductId }); } break;
            }
            
            return RedirectToAction("CreateProductVariation",new{ baseProductId=product.ProductId});
        }

        public IActionResult ProductVariation(int productVariationID)
        {
            var pv = db.ProductVariations.Include(x=>x.ColorVariation).ThenInclude(x=>x.ImageURLs)
                .Include(x=>x.BaseProduct).ThenInclude(x=>x.Category).Include(x=>x.SizeVariation).
                ThenInclude(x=>x.ListParams).FirstOrDefault(x => x.ProductVariationId == productVariationID);
            var vm = new SkirtProducViewModel();
            if (_signInManager.IsSignedIn(User))
            {
                var userID = _userManager.GetUserId(HttpContext.User);
                var user = db.Users.Include(x => x.UserSizes).FirstOrDefault(x=>x.Id==userID);
                if (pv.BaseProduct.Category.DressType == DressTypeEnum.DressAndSkirt)
                {
                    //if(user.UserSizes.Breast)
                    pv = SizeResponseController.CalculateRating(pv, user.UserSizes);
                }

                vm.User = user;
                var bestRating = pv.SizeVariation.Max(x => x.Rating);
                vm.BestSize = pv.SizeVariation.FirstOrDefault(x => Math.Abs(x.Rating - bestRating) < 0.01);
            }
            vm.Product = pv;
            return View(vm);
        }
        [HttpGet]
        public IActionResult CreateProductVariation(int? baseProductId=null)
        {
            var productVariation = new ProductVariation();
            productVariation.BaseProduct = db.Products.FirstOrDefault(x=>x.ProductId==baseProductId);
            db.ProductVariations.Add(productVariation);
            db.SaveChanges();
            return View(productVariation);
        }
        [HttpGet]
        public IActionResult CreateSizeVariation(int? productVariationId = null)
        {
            var productVariation = db.ProductVariations.Include(x=>x.BaseProduct).FirstOrDefault(x => x.ProductVariationId == productVariationId);
            return View(productVariation);
        }

        public IActionResult AddSizeVariation(int? productVariationId = null, string namedSize = null, double? breastSize = null, double? breastMaxSize = null, double? girthHandsSize = null,
            double? girthOfThighsMaxSize = null, double? girthOfThighsSize = null, double? height = null, double? hipsSize = null, double? hipsMaxSize = null,
            double? waistSize = null, double? waistMaxSize = null, double? widthOfTheShoulders = null, double? widthOfTheShouldersMax = null, double? price = null, int? countInStore = null)
        {
            var productVariation = db.ProductVariations.Include(x=>x.SizeVariation).FirstOrDefault(x => x.ProductVariationId == productVariationId);
            var sv = new SizeVariation()
            {
                NamedSize = (NamedSizeEnum)Enum.Parse(typeof(NamedSizeEnum), namedSize, true),
            };
            sv.ListParams = new List<Size>();
            sv.ListParams.Add(new Size() { TypeSize = SizesEnum.Breast, Value = breastSize, MaxValue = breastMaxSize });
            sv.ListParams.Add(new Size() { TypeSize = SizesEnum.Height, Value = height });
            sv.ListParams.Add(new Size() { TypeSize = SizesEnum.GirthHands, Value = girthHandsSize });
            sv.ListParams.Add(new Size() { TypeSize = SizesEnum.GirthOfThighs, Value = girthOfThighsSize, MaxValue = girthOfThighsMaxSize });
            sv.ListParams.Add(new Size() { TypeSize = SizesEnum.Hips, Value = hipsSize, MaxValue = hipsMaxSize });
            sv.ListParams.Add(new Size() { TypeSize = SizesEnum.Waist, Value = waistSize, MaxValue = waistMaxSize });
            sv.ListParams.Add(new Size() { TypeSize = SizesEnum.WidthOfTheShoulders, Value = widthOfTheShoulders, MaxValue = widthOfTheShouldersMax });
            sv.CountInStore = (int)countInStore;
            productVariation.SizeVariation.Add(sv);
            db.ProductVariations.Update(productVariation);
            db.SaveChanges();
            return RedirectToAction("ProductVariation", new {productVariationID = productVariation.ProductVariationId});
        }

        [HttpGet]
        public IActionResult CreateSkirt(int? baseProductId = null)
        {
            var productVariation = new ProductVariation();
            productVariation.BaseProduct = db.Products.FirstOrDefault(x => x.ProductId == baseProductId);
            db.ProductVariations.Add(productVariation);
            db.SaveChanges();
            return View(productVariation);
        }

        [HttpGet]
        public IActionResult CreateTshirt(int? baseProductId = null)
        {
            var productVariation = new ProductVariation();
            productVariation.BaseProduct = db.Products.FirstOrDefault(x => x.ProductId == baseProductId);
            db.ProductVariations.Add(productVariation);
            db.SaveChanges();
            return View(productVariation);
        }

        public IActionResult NewProductVariation(int? productVariationId = null,string color=null,
            string namedSize=null,double? breastSize=null, double? breastMaxSize = null, double? girthHandsSize = null,
            double? girthOfThighsMaxSize = null,double? girthOfThighsSize = null,double? height = null,double? hipsSize = null, double? hipsMaxSize = null,
            double? waistSize = null, double? waistMaxSize = null,  double? widthOfTheShoulders = null, double? widthOfTheShouldersMax = null,  double? price = null,int? countInStore=null)
        {

            var pv = db.ProductVariations.Include(x=>x.ColorVariation).ThenInclude(x=>x.ImageURLs).FirstOrDefault(x => x.ProductVariationId == productVariationId);
            pv.ColorVariation.Color = color;
            pv.SizeVariation = new List<SizeVariation>();
            var sv=new SizeVariation(){
                NamedSize = (NamedSizeEnum)Enum.Parse(typeof(NamedSizeEnum), namedSize, true),
            };
            sv.ListParams = new List<Size>();
            sv.ListParams.Add(new Size(){TypeSize = SizesEnum.Breast,Value = breastSize,MaxValue =breastMaxSize });
            sv.ListParams.Add(new Size() { TypeSize = SizesEnum.Height, Value = height });
            sv.ListParams.Add(new Size() { TypeSize = SizesEnum.GirthHands, Value = girthHandsSize });
            sv.ListParams.Add(new Size() { TypeSize = SizesEnum.GirthOfThighs, Value = girthOfThighsSize,MaxValue = girthOfThighsMaxSize});
            sv.ListParams.Add(new Size() { TypeSize = SizesEnum.Hips, Value = hipsSize,MaxValue = hipsMaxSize });
            sv.ListParams.Add(new Size() { TypeSize = SizesEnum.Waist, Value = waistSize,MaxValue = waistMaxSize});
            sv.ListParams.Add(new Size() { TypeSize = SizesEnum.WidthOfTheShoulders, Value = widthOfTheShoulders,MaxValue = widthOfTheShouldersMax });
            sv.CountInStore= (int)countInStore;
            pv.SizeVariation.Add(sv);
            pv.Price = (double)price;
            db.ProductVariations.Update(pv);
            db.SaveChanges();
            return RedirectToAction("ProductVariation","Catalog",new{ productVariationID =pv.ProductVariationId});
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

                    images.Add(new FilePath(){Path = name});

                }
            }
            if(productVariationId != null)
            {
                var pv = db.ProductVariations.Include(x => x.ColorVariation).ThenInclude(x=>x.ImageURLs)
                    .FirstOrDefault(x => x.ProductVariationId == productVariationId);

                if (pv.ColorVariation == null)
                {
                    pv.ColorVariation=new ColorVariation();
                    pv.ColorVariation.ImageURLs = images;
                }
                else
                {
                    pv.ColorVariation.ImageURLs.AddRange(images);
                }

                db.ProductVariations.Update(pv);
            }
            db.SaveChanges();
            return Ok();
        }

        public IActionResult Skirt(double? priceFrom=null,double?priceTo=null,bool?sizeForMe = null,string[]colors=null,string[]brands=null,string[]lengths=null,int? minFit=null)
        {
            var user = db.Users.Include(x => x.UserSizes).FirstOrDefault(x => x.Id == _userManager.GetUserId(HttpContext.User));
            var vm = new SkirtListViewModel();
            vm.AllProducts = db.ProductVariations.Include(x => x.BaseProduct).ThenInclude(x => x.Category).Include(x=>x.ColorVariation).ThenInclude(x=>x.ImageURLs).Include(x=>x.SizeVariation).ThenInclude(x=>x.ListParams)
                .Where(x => x.BaseProduct.Category.DressType == DressTypeEnum.DressAndSkirt).ToList();
           
            vm.PriceFrom=priceFrom ?? vm.AllProducts.Min(x => x.Price);
            vm.PriceTo = priceTo ?? vm.AllProducts.Max(x => x.Price);
            vm.SizeForMe = sizeForMe ?? false;
            vm.AllColors = vm.AllProducts.Select(x => x.ColorVariation.Color).ToList();
            vm.AllBrand = vm.AllProducts.Select(x => x.BaseProduct.Brand).ToList();
            vm.SelectedColors= colors?.ToList() ?? new List<string>();
            vm.SelectedBrand = brands?.ToList() ?? new List<string>();
            vm.SelectedLength = lengths?.ToList() ?? new List<string>();
            vm.MinFit = minFit ?? 1;
            vm.Products = vm.AllProducts;
            vm.Products = vm.Products.Where(x => (x.Price >= vm.PriceFrom) & (x.Price <= vm.PriceTo)).ToList();
            if (vm.SelectedColors.Count != 0)
            {
                vm.Products = vm.Products.Where(x => vm.SelectedColors.Contains(x.ColorVariation.Color)).ToList();
            }

            if (vm.SelectedBrand.Count != 0)
            {
                vm.Products = vm.Products.Where(x => vm.SelectedBrand.Contains(x.BaseProduct.Brand)).ToList();
            }
            if (vm.SelectedLength.Count != 0)
            {
               //Todo
            }

            if (vm.SizeForMe)
            {
                for(int i=0;i<vm.Products.Count;i++)
                {
                    vm.Products[i] = SizeResponseController.CalculateRating(vm.Products[i],user.UserSizes);
                }
                vm.Products = vm.Products.Where(x => x.SizeVariation.Count(i => i.Rating >= vm.MinFit) > 0).ToList();
            }

            return View(vm);
        }
        
       
    }
}