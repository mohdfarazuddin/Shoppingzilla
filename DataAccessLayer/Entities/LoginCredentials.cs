using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class LoginCredentials
    {

        public Guid Id { get; set; }

        public string EmailID { get; set; }

        public string PasswordHash { get; set; }

        public UserDetails User { get; set; }

    }
}
