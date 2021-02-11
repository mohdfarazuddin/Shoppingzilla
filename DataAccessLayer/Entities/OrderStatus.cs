using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class OrderStatus
    {
        public Guid Id { get; set; }

        public string Status { get; set; }

        public IList<OrderDetails> Orders { get; set; } = new List<OrderDetails>();

    }
}
