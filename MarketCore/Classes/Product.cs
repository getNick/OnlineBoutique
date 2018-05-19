using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using MarketCore.Interfaces;

namespace MarketCore.Classes
{
    public class Product :ModelBase
    {
        public FilePath ImageUrl { get; set; }
        [NotMapped]
        public double Price
        {
            get { return 0; }
        }
        [NotMapped]
        public int Count
        {
            get { return 0; }
        }
        public Category Category { get; set; }
    }
}
