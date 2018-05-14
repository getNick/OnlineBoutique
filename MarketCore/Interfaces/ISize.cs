using System;
using System.Collections.Generic;
using System.Text;
using MarketCore.EmunsAndConst;

namespace MarketCore.Interfaces
{
    interface ISize
    {
        SizesEnum TypeSize { get; set; }
        double Value { get; set; }
    }
}
