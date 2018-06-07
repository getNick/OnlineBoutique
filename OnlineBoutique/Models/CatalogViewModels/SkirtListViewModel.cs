using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketCore.Classes;
using MarketCore.EmunsAndConst;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineBoutique.Models.CatalogViewModels
{
    public class SkirtListViewModel
    {
        public List<ProductVariation> Products { get; set; }
        public List<ProductVariation> AllProducts { get; set; }
        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }
        public bool SizeForMe { get; set; }
        public int MinFit { get; set; }
        public List<string> AllColors { get; set; }
        public List<string> SelectedColors { get; set; }
        public List<string> AllBrand { get; set; }
        public List<string> SelectedBrand { get; set; }
        
        public List<string> AllLength = new List<string>()
        {
            Constants.LengthMini,
            Constants.LengthMidi,
            Constants.LengthMaxi,
        };
        public List<string> SelectedLength { get; set; }


    }
}
