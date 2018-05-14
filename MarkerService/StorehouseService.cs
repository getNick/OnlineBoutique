using System;
using System.Collections.Generic;
using System.Linq;
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
        public void AddProduct(Storehouse storehouse,ProductVariation productVariation)
        {
            if (storehouse.Products.Contains(productVariation.BaseProduct))
            {
                productVariation.BaseProduct.ProductVariations.Add(productVariation);
            }
        }

        public List<Product> GetAllProducts()
        {
            var listProducts=new List<Product>();
            foreach (var storehouse in Storehouses)
            {
                listProducts.AddRange(storehouse.Products);
            }
            return listProducts;
        }
    }
}
