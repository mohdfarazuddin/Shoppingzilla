using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Roles
    {

        public int Id { get; set; }

        public string RoleType { get; set; }

        public IList<UserDetails> Users { get; set; } = new List<UserDetails>();

    }
}
