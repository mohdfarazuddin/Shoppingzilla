using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Categories
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<SubCategories> SubCategories { get; set; } = new List<SubCategories>();

    }
}
