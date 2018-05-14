using System;
using System.Collections.Generic;
using System.Text;

namespace MarketCore.Interfaces
{
    public interface IProduct
    {
        int Id { get; set; }
        string Name { get;}
        string Description { get;}
        double Price { get; }
    }
}
