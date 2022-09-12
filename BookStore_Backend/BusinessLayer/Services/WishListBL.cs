using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL : IWishListBL
    {
        private readonly IWishListRL wishListRL;

        public WishListBL(IWishListRL wishListRL)
        {
            this.wishListRL = wishListRL;
        }

        public bool AddToWishList(int UserId, WishListPostModel listPostModel)
        {
            try
            {
                return this.wishListRL.AddToWishList(UserId, listPostModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WishListResponseModel> GetAllWishList(int UserId)
        {
            try
            {
                return this.wishListRL.GetAllWishList(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteWishListItem(int UserId, int WishListId)
        {
            try
            {
                return this.wishListRL.DeleteWishListItem(UserId, WishListId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WishListResponseModel GetByWishListId(int WishListId, int UserId)
        {
            try
            {
                return this.wishListRL.GetByWishListId(UserId, WishListId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
