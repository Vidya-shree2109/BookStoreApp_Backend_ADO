using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRL
    {
        public bool AddBookToCart(int UserId, CartDataModel postModel);
        public List<CartModel> GetAllBooksInCart(int UserId);
        public bool UpdateCart(int UserId, CartUpdateModel cartUpdateModel);
        public bool DeleteCartItembyBookId(int UserId, int CartId);
        public CartModel GetCartItemByCartId(int CartId, int UserId);
    }
}
