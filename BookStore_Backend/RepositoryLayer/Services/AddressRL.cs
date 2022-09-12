using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AddressRL : IAddressRL
    {
        private readonly string connectionString;

        public AddressRL(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStoreApp");
        }

        public bool AddAddress(int UserId, AddressPostModel postModel)
        {
            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlconnection)
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand("AddAddressSP", sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AddressType", postModel.AddressType);
                    cmd.Parameters.AddWithValue("@FullAddress", postModel.FullAddress);
                    cmd.Parameters.AddWithValue("@City", postModel.City);
                    cmd.Parameters.AddWithValue("@State", postModel.State);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        public List<AddressResponseModel> GetAllAddress(int UserId)
        {
            List<AddressResponseModel> list = new List<AddressResponseModel>();
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("GetAllAddressSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        AddressResponseModel addressdetails = new AddressResponseModel();
                        addressdetails.AddressId = reader["AddressId"] == DBNull.Value ? default : reader.GetInt32("AddressId");
                        addressdetails.UserId = UserId;
                        addressdetails.FullName = reader["FullName"] == DBNull.Value ? default : reader.GetString("FullName");
                        addressdetails.MobileNo = reader["Phone"] == DBNull.Value ? default : reader.GetInt64("Phone");
                        addressdetails.AddressType = reader["AddressType"] == DBNull.Value ? default : reader.GetInt32("AddressType");
                        addressdetails.FullAddress = reader["FullAddress"] == DBNull.Value ? default : reader.GetString("FullAddress");
                        addressdetails.City = reader["City"] == DBNull.Value ? default : reader.GetString("City");
                        addressdetails.State = reader["State"] == DBNull.Value ? default : reader.GetString("State");
                        list.Add(addressdetails);
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool DeleteAddressByAddressId(int AddressId, int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("DeleteAddressByIdSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AddressId ", AddressId);
                    cmd.Parameters.AddWithValue("@UserId ", UserId);
                    var result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool UpdateAddressbyId(int UserId, AddressPutModel postModel)
        {
            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            try
            {
                {
                    sqlconnection.Open();

                    SqlCommand cmd = new SqlCommand("UpdateAddressbyIdSP", sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AddressId", postModel.AddressId);
                    cmd.Parameters.AddWithValue("@AddressType", postModel.AddressType);
                    cmd.Parameters.AddWithValue("@FullAddress", postModel.FullAddress);
                    cmd.Parameters.AddWithValue("@City", postModel.City);
                    cmd.Parameters.AddWithValue("@State", postModel.State);

                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        public AddressResponseModel GetAddressById(int AddressId, int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("GetAddressByIdSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AddressId", AddressId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    AddressResponseModel addressdetails = new AddressResponseModel();
                    if (reader.Read())
                    {
                        addressdetails.AddressId = reader["AddressId"] == DBNull.Value ? default : reader.GetInt32("AddressId");
                        addressdetails.UserId = UserId;
                        addressdetails.FullName = reader["FullName"] == DBNull.Value ? default : reader.GetString("FullName");
                        addressdetails.MobileNo = reader["Phone"] == DBNull.Value ? default : reader.GetInt64("Phone");
                        addressdetails.AddressType = reader["AddressType"] == DBNull.Value ? default : reader.GetInt32("AddressType");
                        addressdetails.FullAddress = reader["FullAddress"] == DBNull.Value ? default : reader.GetString("FullAddress");
                        addressdetails.City = reader["City"] == DBNull.Value ? default : reader.GetString("City");
                        addressdetails.State = reader["State"] == DBNull.Value ? default : reader.GetString("State");
                    }

                    if (addressdetails.AddressId == 0)
                    {
                        return null;
                    }

                    return addressdetails;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
