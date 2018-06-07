using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text;
using MarketCore.EmunsAndConst;

namespace MarketCore.Classes
{
    public class ColorVariation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ColorVariationId { get; set; }
        public string Color{ get; set; }
        public List<FilePath> ImageURLs { get; set; }
    }
}
