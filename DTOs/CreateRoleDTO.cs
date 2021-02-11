using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CreateRoleDTO
    {
        [Required]
        public string RoleType { get; set; }
    }
}
