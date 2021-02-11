using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Products
    {

        public Guid Id { get; set; }

        public Guid BrandID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Brands Brand { get; set; }

        public IList<Models> Models { get; set; } = new List<Models>();

    }
}
