using System;
using System.Collections.Generic;
using System.Text;

namespace MarketCore.Classes
{
    public class Category:ModelBase
    {
        public List<Product> ListProducts { get; set; }
    }
}
