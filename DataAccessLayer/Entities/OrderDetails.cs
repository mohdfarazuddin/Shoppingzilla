using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class OrderDetails
    {

        public Guid Id { get; set; }

        public Guid UserID { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime Date { get; set; }

        public Guid DeliveryAddID { get; set; }

        public Guid PaymentModeID { get; set; }

        public int TransactionID { get; set; }

        public Guid OrderStatusID { get; set; }
        
        public UserDetails User { get; set; }

        public DeliveryAddress DeliveryAddress { get; set; }

        public IList<OrderItems> OrderItems { get; set; } = new List<OrderItems>();

        public PaymentModes PaymentType { get; set; }

        public OrderStatus OrderStatus { get; set; }

    }
}
