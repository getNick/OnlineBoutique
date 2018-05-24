using System;
using System.Collections.Generic;
using System.Text;

namespace MarketCore.Classes
{
    public class TempProductStore
    {

        private static TempProductStore instance;

        private TempProductStore()
        { }

        public static TempProductStore getInstance()
        {
            if (instance == null)
                instance = new TempProductStore();
            return instance;
        }

        public List<Product> Products=new List<Product>();
        public List<ProductVariation> ProductVariations=new List<ProductVariation>();
    }
}
