using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class OrderPostModel
    {
        [Required]
        public int CartId { get; set; }

        [Required]
        public int AddressId { get; set; }
    }
}
