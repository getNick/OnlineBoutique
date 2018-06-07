using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MarketCore.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace OnlineBoutique.Models.Classes
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public Order Order { get; set; }
        public ProductVariation ProductVariation { get; set; }
        public int Count { get; set; }
        [NotMapped]
        public double Sum { get; set; }
    }
}
