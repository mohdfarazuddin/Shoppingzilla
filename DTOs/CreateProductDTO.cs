using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CreateProductDTO
    {
        public Guid BrandID { get; set; }

        public Guid SubCategoryID { get; set; }

        public Guid CategoryID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
