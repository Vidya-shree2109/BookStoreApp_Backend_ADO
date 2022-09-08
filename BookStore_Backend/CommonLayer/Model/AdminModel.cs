using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class AdminModel
    {
        [Required]
        [DefaultValue("Shree123@gmail.com")]
        public string EmailId { get; set; }

        [Required]
        [DefaultValue("Shree@1234")]
        public string Password { get; set; }
    }
}
