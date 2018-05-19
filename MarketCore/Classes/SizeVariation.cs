using System;
using System.Collections.Generic;
using System.Text;
using MarketCore.EmunsAndConst;

namespace MarketCore.Classes
{
    public class SizeVariation
    {
        public int Id { get; set; }
        public NamedSizeEnum NamedSize { get; set; }
        public List<Size> ListParams { get; set; }
    }
}
