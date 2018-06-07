using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MarketCore.EmunsAndConst;

namespace MarketCore.Classes
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public GenderCategoriesEnum? Gender { get; set; }
        public DressTypeEnum? DressType { get; set; }
        public StyleEnum? Style { get; set; }
        public int? Year { get; set; }
        
    }
}
