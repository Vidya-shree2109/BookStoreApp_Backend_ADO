using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class CartPostModel
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        [DefaultValue("1")]
        [Range(1, 1000, ErrorMessage = "Book Quantity Exceeded it limit!!")]
        public int BookQuantity { get; set; }
    }
}
