using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class CartModel
    {

        public int CartId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public string BookName { get; set; }

        public string Author { get; set; }

        public int BookQuantity { get; set; }

        public int Price { get; set; }

        public int DiscountPrice { get; set; }

        public string BookImage { get; set; }
    }
}
