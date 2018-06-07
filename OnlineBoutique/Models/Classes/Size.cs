using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MarketCore.EmunsAndConst;

namespace MarketCore.Classes
{
    public class Size
    {
        [Key]
        public int SizeId { get; set; }
        public SizeVariation SizeVariation { get; set; }
        public SizesEnum? TypeSize { get; set; }
        public double? MaxValue { get; set; }
        public double? Value { get; set; }
        [NotMapped]
        public double Rating { get; set; }
        [NotMapped]
        public string SizeResponse { get; set; }
        [NotMapped]
        public double UserSize { get; set; }
    }
}
