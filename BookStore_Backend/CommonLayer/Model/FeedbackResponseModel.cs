using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class FeedbackResponseModel
    {
        public int FeedbackId { get; set; }

        public string Comment { get; set; }

        public double Rating { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public string FullName { get; set; }
    }
}
