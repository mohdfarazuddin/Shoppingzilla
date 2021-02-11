using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class CartItems
    {

        public Guid UserID { get; set; }

        public Guid ModelID { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public UserDetails User { get; set; }

        public Models Model { get; set; }
    }
}
