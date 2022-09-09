using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class BookModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public double TotalRating { get; set; }
        public int RatingCount { get; set; }
        public string BookImage { get; set; }
    }
}
