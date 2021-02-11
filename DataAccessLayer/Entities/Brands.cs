using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Brands
    {

        public Guid Id { get; set; }

        public Guid SubCategoryID { get; set; }

        public string Name { get; set; }

        public SubCategories SubCategory { get; set; }

        public IList<Products> Products { get; set; } = new List<Products>();

    }
}
