using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BookStore_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL feedbackBL;

        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }

        [Authorize]
        [HttpPost("AddFeedback")]
        public IActionResult AddFeedback(FeedbackPostModel postModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = this.feedbackBL.AddFeedback(UserId, postModel);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"You have already added Feedback for BookId : {postModel.BookId}!!" });
                }

                return this.Ok(new { success = true, Message = $"Feedback added for BookId : {postModel.BookId} Added Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetAllFeedbacks/{BookId}")]
        public IActionResult GetAllFeddbacksByBookId(int BookId)
        {
            try
            {
                List<FeedbackResponseModel> result = this.feedbackBL.GetAllFeedbacksByBookId(BookId);
                if (result.Count == 0)
                {
                    return this.BadRequest(new { success = false, Message = $"No Feedbacks available for BookId:{BookId} !!" });
                }

                return this.Ok(new { success = true, Message = $"All Feedbacks fetched for BookId : {BookId} Sucessfully...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteFeedback/{FeedbackId}")]
        public IActionResult DeleteFeedbackById(int FeedbackId)
        {
            try
            {
                var result = this.feedbackBL.DeleteFeedbackById(FeedbackId);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Something went wrong while removing FeedbackId : {FeedbackId} from the Feedback List!!" });
                }

                return this.Ok(new { success = true, Message = $"FeedbackId : {FeedbackId} removed from Feedback List Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
