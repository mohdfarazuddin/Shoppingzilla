using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class SubCategoryDTO
    {
        public Guid Id { get; set; }

        public Guid CategoryID { get; set; }

        public string Name { get; set; }

        public IList<BrandDTO> Brands { get; set; } = new List<BrandDTO>();
    }
}
