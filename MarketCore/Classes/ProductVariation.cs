using System;
using System.Collections.Generic;
using System.Text;

namespace MarketCore.Classes
{
    public class ProductVariation
    {
        public Product BaseProduct { get; set; }
        public ColorVariation ColorVariation { get; set; }
        public SizeVariation SizeVariation { get; set; }
        public double Price { get; set; }
        public int CountInStore { get; set; }
    }
}
