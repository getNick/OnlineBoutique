using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarketCore.Classes
{
    public class ProductVariation
    {
        [Key]
        public int ProductVariationId { get; set; }
        public Product BaseProduct { get; set; }
        public ColorVariation ColorVariation { get; set; }
        public List<SizeVariation> SizeVariation { get; set; }
        public double Price { get; set; }
    }
}
