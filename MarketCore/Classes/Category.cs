using System;
using System.Collections.Generic;
using System.Text;
using MarketCore.EmunsAndConst;

namespace MarketCore.Classes
{
    public class Category
    {
        public int Id { get; set; }
        public GenderCategoriesEnum Gender { get; set; }
        public SeasonCategoriesEnum Season { get; set; }
        public DressTypeEnum DressType { get; set; }
        private int? _year;
        public int? Year
        {
            get { return _year;}
            set
            {
                if ((value > 1950) & (value < 2050))
                {
                    _year = value;
                }
            }
        }
    }
}
