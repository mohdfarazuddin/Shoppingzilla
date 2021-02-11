using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class PaymentModes
    {

        public Guid Id { get; set; }

        public string PaymentType { get; set; }

        public IList<OrderDetails> Orders { get; set; } = new List<OrderDetails>();

    }
}
