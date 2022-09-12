using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeedbackBL : IFeedbackBL
    {
        private readonly IFeedbackRL feedbackRL;

        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }

        public bool AddFeedback(int UserId, FeedbackPostModel postModel)
        {
            try
            {
                return this.feedbackRL.AddFeedback(UserId, postModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FeedbackResponseModel> GetAllFeedbacksByBookId(int BookId)
        {
            try
            {
                return this.feedbackRL.GetAllFeedbacksByBookId(BookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteFeedbackById(int FeedbackId)
        {
            try
            {
                return this.feedbackRL.DeleteFeedbackById(FeedbackId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
