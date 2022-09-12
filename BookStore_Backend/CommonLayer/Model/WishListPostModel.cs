using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class WishListPostModel
    {
        [Required]
        public int BookId { get; set; }
    }
}
