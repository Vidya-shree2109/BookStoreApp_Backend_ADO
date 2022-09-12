using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressRL
    {
        public bool AddAddress(int UserId, AddressPostModel postModel);

        public List<AddressResponseModel> GetAllAddress(int UserId);

        public bool DeleteAddressByAddressId(int AddressId, int UserId);

        public bool UpdateAddressbyId(int UserId, AddressPutModel postModel);

        public AddressResponseModel GetAddressById(int AddressId, int UserId);
    }
}
