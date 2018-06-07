using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketCore.Classes;
using Microsoft.AspNetCore.Identity;

namespace OnlineBoutique.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public UserSizes UserSizes { get; set; }
        public List<Order> Orders { get; set; }
    }
}
