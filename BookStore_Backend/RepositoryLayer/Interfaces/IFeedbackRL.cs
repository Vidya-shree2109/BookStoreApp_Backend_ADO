using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IFeedbackRL
    {
        public bool AddFeedback(int UserId, FeedbackPostModel postModel);

        public List<FeedbackResponseModel> GetAllFeedbacksByBookId(int BookId);

        public bool DeleteFeedbackById(int FeedbackId);
    }
}
