using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketCore.Classes;

namespace OnlineBoutique.Models.CatalogViewModels
{
    public class SkirtProducViewModel
    {
        public ProductVariation Product { get; set; }
        public ApplicationUser User { get; set; }
        public SizeVariation BestSize { get; set; }
        public bool HaveAllRequiredSizes { get; set; } = false;
    }
}
