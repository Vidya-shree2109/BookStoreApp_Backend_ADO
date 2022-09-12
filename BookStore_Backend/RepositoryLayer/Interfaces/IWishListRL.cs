using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IWishListRL
    {
        public bool AddToWishList(int UserId, WishListPostModel listPostModel);

        public List<WishListResponseModel> GetAllWishList(int UserId);

        public bool DeleteWishListItem(int UserId, int WishListId);

        public WishListResponseModel GetByWishListId(int WishListId, int UserId);
    }
}
