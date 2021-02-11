using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<SubCategoryDTO> SubCategories { get; set; } = new List<SubCategoryDTO>();
    }
}
