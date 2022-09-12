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
    [Authorize(Roles = Role.User)]

    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListBL wishListBL;

        public WishListController(IWishListBL wishListBL)
        {
            this.wishListBL = wishListBL;
        }

        [HttpPost("AddToWishList")]
        public IActionResult AddToWishList(WishListPostModel listPostModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = this.wishListBL.AddToWishList(UserId, listPostModel);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Check if Book is availbale OR it is already in WishList!!  WishListId : {listPostModel.BookId} to the WishList!!" });
                }

                return this.Ok(new { success = true, Message = $"WishListId : {listPostModel.BookId} Added to WishList Sucessfull..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAllWishList")]
        public IActionResult GetAllWishList()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                List<WishListResponseModel> result = this.wishListBL.GetAllWishList(UserId);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = $"No Book available in WishList!!" });
                }

                return this.Ok(new { success = true, Message = $"Books in WishList fetched Sucessfully...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetByWishListId/{WishListId}")]
        public IActionResult GetByWishListId(int WishListId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = this.wishListBL.GetByWishListId(WishListId, UserId);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = $"No Book available in WishList with this WishListId :{WishListId}!!" });
                }

                return this.Ok(new { success = true, Message = $"WishListId: {WishListId} details fetched Sucessfully...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("DeleteWishListItem/{WishListId}")]
        public IActionResult DeleteWishListItem(int WishListId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = this.wishListBL.DeleteWishListItem(UserId, WishListId);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Something went wrong while removing WishListId : {WishListId} from the WishList!!" });
                }

                return this.Ok(new { success = true, Message = $"WishListId : {WishListId} removed from WishList Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
