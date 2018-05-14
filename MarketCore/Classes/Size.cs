using System;
using System.Collections.Generic;
using System.Text;
using MarketCore.EmunsAndConst;
using MarketCore.Interfaces;

namespace MarketCore.Classes
{
    public class Size:ISize
    {
        public SizesEnum TypeSize { get; set; }
        public double Value { get; set; }
    }
}
