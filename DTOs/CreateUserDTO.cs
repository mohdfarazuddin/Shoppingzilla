using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string MobileNo { get; set; }

        [Required]
        public string EmailID { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string password2 { get; set; } 

    }
}
