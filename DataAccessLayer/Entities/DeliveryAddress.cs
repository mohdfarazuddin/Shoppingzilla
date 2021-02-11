using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class DeliveryAddress
    {

        public Guid Id { get; set; }

        public Guid UserID { get; set; }

        public string AddressLine { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PinCode { get; set; }

        public UserDetails User { get; set; }

        public IList<OrderDetails> Orders { get; set; } = new List<OrderDetails>();


    }
}
