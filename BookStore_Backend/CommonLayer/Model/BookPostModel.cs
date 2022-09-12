using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class BookPostModel
    {
        [Required]
        [DefaultValue("")]
        public string BookName { get; set; }

        [Required]
        [DefaultValue("")]
        public string Author { get; set; }

        [Required]
        [DefaultValue("")]
        public string Description { get; set; }

        [Required]
        [DefaultValue(0)]
        public int Quantity { get; set; }

        [Required]
        [DefaultValue(0)]
        [Range(1, 100000, ErrorMessage = "Book Price must be in Range: 1-100000")]
        public decimal Price { get; set; }

        [Required]
        [DefaultValue(0)]
        [Range(1, 100000, ErrorMessage = "Book Discounted Price must be in Range: 1-100000")]
        public decimal DiscountPrice { get; set; }

        [Required]
        [DefaultValue(0)]
        [Range(0, 5.0, ErrorMessage = "Book Discounted Price must be in Range: 0-5.0")]
        public decimal TotalRating { get; set; }

        [Required]
        [DefaultValue(0)]
        public int RatingCount { get; set; }

        [Required]
        [DefaultValue("")]
        public string BookImg { get; set; }
    }
}
