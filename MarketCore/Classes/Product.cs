using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using MarketCore.Interfaces;

namespace MarketCore.Classes
{
    public class Product :ModelBase
    {
        public FilePath ImageUrl { get; set; }
        [NotMapped]
        public double Price
        {
            get
            {
                if ((ProductVariations!=null)&&(ProductVariations.Count > 0))
                {
                    return ProductVariations.Min(x => x.Price);
                }
                return 0;
            }
        }
        [NotMapped]
        public int Count
        {
            get
            {
                if ((ProductVariations != null) && (ProductVariations.Count > 0))
                {
                    return ProductVariations.Sum(x => x.CountInStore);
                }
                return 0;
            }
        }
        public Category Category { get; set; }
        [NotMapped]
        public List<ProductVariation> ProductVariations { get; set; }
    }
}
