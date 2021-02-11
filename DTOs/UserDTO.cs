using System;

namespace DTOs
{
    public class UserDTO
    {
        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }

        public Guid UserID { get; set; }

        public string EmailID { get; set; }

        public int RoleID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobileNo { get; set; }


    }
}
