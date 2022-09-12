using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IWishListBL
    {
        public bool AddToWishList(int UserId, WishListPostModel listPostModel);

        public List<WishListResponseModel> GetAllWishList(int UserId);

        public bool DeleteWishListItem(int UserId, int WishListId);

        public WishListResponseModel GetByWishListId(int WishListId, int UserId);
    }
}
