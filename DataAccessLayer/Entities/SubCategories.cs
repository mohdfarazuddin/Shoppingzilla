using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class SubCategories
    {

        public Guid Id { get; set; }

        public Guid CategoryID { get; set; }

        public string Name { get; set; }

        public Categories Category { get; set; }

        public IList<Brands> Brands { get; set; } = new List<Brands>();

    }
}
