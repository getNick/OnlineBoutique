using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MarketCore.EmunsAndConst;

namespace MarketCore.Classes
{
    public class SizeVariation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SizeVariationId { get; set; }
        public ProductVariation ProductVariation { get; set; }
        public NamedSizeEnum? NamedSize { get; set; }
        public List<Size> ListParams { get; set; }
        public int CountInStore { get; set; }
        [NotMapped]
        public double Rating { get; set; }
        [NotMapped]
        public string SizeResponse { get; set; }
    }
}
