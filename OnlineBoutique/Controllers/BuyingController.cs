using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketCore.Classes;
using MarketCore.EmunsAndConst;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBoutique.Data;
using OnlineBoutique.Models;
using OnlineBoutique.Models.Classes;

namespace OnlineBoutique.Controllers
{
    public class BuyingController : Controller
    {
        private ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public BuyingController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            db = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Backet()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = db.Users.Include(x => x.UserSizes).FirstOrDefault(x => x.Id == userId);
            var activeBacket = db.Orders.Include(x => x.Customer).Include(x => x.OrderItems).
                ThenInclude(x => x.ProductVariation).ThenInclude(x => x.BaseProduct).Include(x => x.OrderItems).
                ThenInclude(x => x.ProductVariation).ThenInclude(x => x.ColorVariation).
                ThenInclude(x => x.ImageURLs).FirstOrDefault(x => (x.Customer.Id == user.Id) & (x.OrderStatus == OrderStatusEnum.Basket));
            if(activeBacket==null) return View(activeBacket);
            for (int i = 0; i < activeBacket.OrderItems.Count; i++)
            {
                activeBacket.OrderItems[i].Sum =
                    activeBacket.OrderItems[i].Count * activeBacket.OrderItems[i].ProductVariation.Price;
            }
            activeBacket.Sum = activeBacket.OrderItems.Sum(x => x.Sum);
            return View(activeBacket);
        }

        public IActionResult DeleteFromBacket(int? orderItemId)
        {
            if(orderItemId==null) return RedirectToAction("Backet");
            var orderItem = db.OrderItems.Include(x=>x.Order).FirstOrDefault(x => x.OrderItemId == orderItemId);
            orderItem.Order.OrderItems.Remove(orderItem);
            db.Update(orderItem.Order);
            db.OrderItems.Remove(orderItem);
            db.SaveChanges();
            return RedirectToAction("Backet");
        }
        public IActionResult AddProductToBacket(int productVariationId)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = db.Users.Include(x => x.UserSizes).FirstOrDefault(x => x.Id == userId);
            var activeBacket = db.Orders.Include(x=>x.Customer).Include(x=>x.OrderItems).
                ThenInclude(x=>x.ProductVariation).ThenInclude(x=>x.BaseProduct).Include(x => x.OrderItems).
                ThenInclude(x => x.ProductVariation).ThenInclude(x=>x.ColorVariation).
                ThenInclude(x=>x.ImageURLs).FirstOrDefault(x => (x.Customer.Id == user.Id)&(x.OrderStatus==OrderStatusEnum.Basket));
            if (activeBacket == null)
            {
                activeBacket = new Order()
                {
                    Customer = user,
                    OrderStatus = OrderStatusEnum.Basket,
                    OrderItems = new List<OrderItem>(),
                };
            }

            var orderItem = new OrderItem()
            {
                Count = 1,
                ProductVariation = db.ProductVariations.Include(x=>x.ColorVariation).ThenInclude(x=>x.ImageURLs).FirstOrDefault(x => x.ProductVariationId == productVariationId),
            };
            
            activeBacket.OrderItems.Add(orderItem);
            for (int i = 0; i < activeBacket.OrderItems.Count; i++)
            {
                activeBacket.OrderItems[i].Sum =
                    activeBacket.OrderItems[i].Count * activeBacket.OrderItems[i].ProductVariation.Price;
            }

            activeBacket.Sum = activeBacket.OrderItems.Sum(x => x.Sum);
            db.Orders.Update(activeBacket);
            db.SaveChanges();
            return View(activeBacket);
        }

        public IActionResult CheckoutOrder(int orderId)
        {
            var activeBacket = db.Orders.Include(x => x.Customer).Include(x => x.OrderItems)
                .ThenInclude(x => x.ProductVariation).FirstOrDefault(x => x.OrderId == orderId);
            for (int i = 0; i < activeBacket.OrderItems.Count; i++)
            {
                activeBacket.OrderItems[i].Sum =
                    activeBacket.OrderItems[i].Count * activeBacket.OrderItems[i].ProductVariation.Price;
            }

            activeBacket.Sum = activeBacket.OrderItems.Sum(x => x.Sum);
            activeBacket.OrderStatus = OrderStatusEnum.WaitConfirm;
            db.Orders.Update(activeBacket);
            db.SaveChanges();
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = db.Users.Include(x => x.UserSizes).FirstOrDefault(x => x.Id == userId);
            return View(user);
        }

        public IActionResult ConfirmOrder(string fio=null,string telNumber=null,string adress=null)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = db.Users.Include(x => x.UserSizes).FirstOrDefault(x => x.Id == userId);
            user.PhoneNumber = telNumber;
            user.UserName = fio;
            user.NormalizedUserName = fio.ToUpper();
            db.Users.Update(user);
            return View();
        }
    }
}