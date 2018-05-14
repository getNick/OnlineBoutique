using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketCore.Interfaces;

namespace MarketCore.Classes
{
    public class Product :ModelBase
    {
        public List<ProductVariation> ProductVariations { get; set; }

        public string ImageUrl { get; set; }

        public double Price
        {
            get { return ProductVariations.Min(x => x.Price); }
        }

        public int Count
        {
            get { return ProductVariations.Sum(x => x.CountInStore); }
        }
    }
}
