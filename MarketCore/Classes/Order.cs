using System;
using System.Collections.Generic;
using System.Text;
using MarketCore.EmunsAndConst;

namespace MarketCore.Classes
{
    public class Order
    {
        public int Id { get; set; }
        public User Customer { get; set; }
        public List<ProductVariation> ShoppingList { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
    }
}
