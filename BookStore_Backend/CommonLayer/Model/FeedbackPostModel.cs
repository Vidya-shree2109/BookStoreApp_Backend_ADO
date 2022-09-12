using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class FeedbackPostModel
    {
        [Required]
        [DefaultValue("0")]
        public int BookId { get; set; }

        [Required]
        [DefaultValue("0.0")]
        public decimal Rating { get; set; }

        [Required]
        [DefaultValue("")]
        public string Comment { get; set; }
    }
}
