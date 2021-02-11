using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CreateModelDTO
    {
        public Guid ProductID { get; set; }

        public string? Colour { get; set; }

        public string? Size { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string imgurl { get; set; }
    }
}
