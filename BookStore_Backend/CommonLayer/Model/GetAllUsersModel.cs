using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class GetAllUsersModel
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string EmailId { get; set; }

        public string Password { get; set; }

        public long Phone { get; set; }
    }
}
