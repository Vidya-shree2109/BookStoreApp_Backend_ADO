using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IOrderRL
    {
        public bool AddOrder(OrderPostModel postModel);

        public List<OrderResponseModel> GetAllOrders(int UserId);
    }
}
