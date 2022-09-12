using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IOrderBL
    {
        public bool AddOrder(OrderPostModel postModel);

        public List<OrderResponseModel> GetAllOrders(int UserId);
    }
}
