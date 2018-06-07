using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MarketCore.EmunsAndConst;
using OnlineBoutique.Models;
using OnlineBoutique.Models.Classes;

namespace MarketCore.Classes
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public ApplicationUser Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderStatusEnum? OrderStatus { get; set; }
        [NotMapped]
        public double Sum { get; set; }

    }
}
