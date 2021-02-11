using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class UserDetails
    {

        public Guid UserID { get; set; }

        public int RoleID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string MobileNo { get; set; }

        public Roles Role { get; set; }

        public LoginCredentials Logincredential { get; set; }

        public IList<DeliveryAddress> DeliveryAddresses { get; set; } = new List<DeliveryAddress>();

        public IList<CartItems> CartItems { get; set; } = new List<CartItems>();

        public IList<OrderDetails> Orders { get; set; } = new List<OrderDetails>();

        public RefreshTokens RefreshToken { get; set; }


    }
}
