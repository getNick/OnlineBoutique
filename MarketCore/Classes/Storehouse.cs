using System;
using System.Collections.Generic;
using System.Text;

namespace MarketCore.Classes
{
    public class Storehouse
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public string Adress { get; set; }
        public string ContactNumber { get; set; }
    }
}
