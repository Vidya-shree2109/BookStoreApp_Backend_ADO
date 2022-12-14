using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICartBL
    {
        public bool AddBookToCart(int UserId, CartPostModel postModel);
        public List<CartResponseModel> GetAllBooksInCart(int UserId);

        public bool UpdateCartItem(int UserId, CartUpdateModel cartUpdateModel);

        public bool DeleteCartItembyBookId(int UserId, int CartId);

        public CartResponseModel GetCartItemByCartId(int CartId, int UserId);

    }
}
