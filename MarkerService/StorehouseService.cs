using System;
using System.Collections.Generic;
using System.Text;
using MarketCore.Classes;

namespace MarkerService
{
    class StorehouseService
    {
        private List<Storehouse> Storehouses { get;}

        public StorehouseService()
        {
            Storehouses=new List<Storehouse>();
        }
        public void AddStorehouse(Storehouse storehouse)
        {
            if(storehouse==null) throw new ArgumentNullException(nameof(storehouse));
            Storehouses.Add(storehouse);
        }

        public void CloseStorehouse(Storehouse storehouse)
        {
            if(storehouse==null)throw new ArgumentNullException(nameof(storehouse));
            storehouse.Products.Clear();
            Storehouses.Remove(storehouse);

        }
        public void AddProduct(Storehouse storehouse,ProductVariation productVariation, int count)
        {
            if (storehouse.Products.ContainsKey(productVariation))
            {
                storehouse.Products[productVariation] += count;
            }
            else
            {
                storehouse.Products.Add(productVariation,count);
            }
        }

        public List<Product> GetProduct(Product product, int count)
        {
            if (!Products.ContainsKey(product)) return null;
            if (Products[product] <= 0) return null;
            if (Products[product] < count)
            {

            }
            return null;
        }
    }
}
