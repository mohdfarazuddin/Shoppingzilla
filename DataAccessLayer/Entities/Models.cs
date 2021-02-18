using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Models
    {

        public Guid Id { get; set; }

        public Guid ProductID { get; set; }

        public Guid BrandID { get; set; }

        public Guid SubCategoryID { get; set; }

        public Guid CategoryID { get; set; }

        public string? Colour { get; set; }

        public string? Size { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string imgurl { get; set; }

        public Products Product { get; set; }

        public IList<CartItems> Carts { get; set; } = new List<CartItems>();

        public IList<OrderItems> Orders { get; set; } = new List<OrderItems>();

    }
}
