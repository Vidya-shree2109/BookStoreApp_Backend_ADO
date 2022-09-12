using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class OrderResponseModel
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public int AddressId { get; set; }

        public string BookName { get; set; }

        public string Author { get; set; }

        public int OrderQuantity { get; set; }

        public decimal TotalOrderPrice { get; set; }

        public string BookImg { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
