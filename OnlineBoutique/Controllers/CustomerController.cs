using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketCore.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBoutique.Data;
using OnlineBoutique.Models;

namespace OnlineBoutique.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CustomerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            db = context;
            _userManager=userManager;
            _signInManager = signInManager;
        }

        public IActionResult AddAllCustomerSizes()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = db.Users.Include(x => x.UserSizes).FirstOrDefault(x=>x.Id==userId);
            if (user.UserSizes == null)
            {
                user.UserSizes = new UserSizes();
                db.Users.Update(user);
                db.SaveChanges();
            }
            return View(user.UserSizes);
        }

        public IActionResult SetSizes(double? breast,double? waist=null,double? thigh=null,
            double? thighGirth=null,double? height=null,double? shouldersWidth=null )
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = db.Users.Include(x => x.UserSizes).FirstOrDefault(x => x.Id == userId);
            var userSizes = user.UserSizes;
            if (breast != null)
            {
                userSizes.Breast = (double)breast;
            }
            if (waist != null)
            {
                userSizes.Waist = (double)waist;
            }
            if (thigh != null)
            {
                userSizes.Thigh = (double)thigh;
            }
            if (thighGirth != null)
            {
                userSizes.ThighGirth = (double)thighGirth;
            }
            if (height != null)
            {
                userSizes.Height = (double)height;
            }
            if (shouldersWidth != null)
            {
                userSizes.ShouldersWidth = (double)shouldersWidth;
            }

            db.Users.Update(user);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}