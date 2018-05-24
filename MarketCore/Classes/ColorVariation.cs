using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using MarketCore.EmunsAndConst;

namespace MarketCore.Classes
{
    public class ColorVariation
    {
        public int Id { get; set; }
        public string Color{ get; set; }
        public List<FilePath> ImageURLs { get; set; }
    }
}
