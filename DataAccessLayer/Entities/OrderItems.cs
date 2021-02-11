using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class OrderItems
    {

        public Guid OrderID { get; set; }

        public Guid ModelID { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public Models Model { get; set; }

        public OrderDetails Order { get; set; }

    }
}
