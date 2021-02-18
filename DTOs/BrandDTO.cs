using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class BrandDTO
    {
        public Guid Id { get; set; }

        public Guid SubCategoryID { get; set; }

        public Guid CategoryID { get; set; }

        public string Name { get; set; }

        public IList<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }
}
