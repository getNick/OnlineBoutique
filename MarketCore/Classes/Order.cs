using System;
using System.Collections.Generic;
using System.Text;

namespace MarketCore.Classes
{
    class Order
    {
        public User Customer { get; set; }
        public List<ProductVariation> ShoppingList { get; set; }
        
    }
}
