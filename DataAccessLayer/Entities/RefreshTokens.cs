using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class RefreshTokens
    {

        public Guid UserID { get; set; }

        public string RefreshToken { get; set; }

        public UserDetails user { get; set; }

    }
}
