using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IFeedbackBL
    {
        public bool AddFeedback(int UserId, FeedbackPostModel postModel);

        public List<FeedbackResponseModel> GetAllFeedbacksByBookId(int BookId);

        public bool DeleteFeedbackById(int FeedbackId);
    }
}
