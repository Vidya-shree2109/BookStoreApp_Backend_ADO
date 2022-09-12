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
    public class OrderRL : IOrderRL
    {
        private readonly string connectionString;

        public OrderRL(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStoreApp");
        }

        public bool AddOrder(OrderPostModel postModel)
        {
            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlconnection)
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand("AddOrderSP", sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CartId", postModel.CartId);
                    cmd.Parameters.AddWithValue("@AddressId", postModel.AddressId);
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0 && result != 1)
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

        public List<OrderResponseModel> GetAllOrders(int UserId)
        {
            List<OrderResponseModel> list = new List<OrderResponseModel>();
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("GetAllOrdersSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        OrderResponseModel order = new OrderResponseModel();
                        order.OrderId = reader["OrderId"] == DBNull.Value ? default : reader.GetInt32("OrderId");
                        order.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        order.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        order.AddressId = reader["AddressId"] == DBNull.Value ? default : reader.GetInt32("AddressId");
                        order.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                        order.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                        order.OrderQuantity = reader["OrderQuantity"] == DBNull.Value ? default : reader.GetInt32("OrderQuantity");
                        order.TotalOrderPrice = reader["TotalOrderPrice"] == DBNull.Value ? default : reader.GetDecimal("TotalOrderPrice");
                        order.OrderDate = reader["OrderDate"] == DBNull.Value ? default : reader.GetDateTime("OrderDate");
                        order.BookImg = reader["BookImg"] == DBNull.Value ? default : reader.GetString("BookImg");
                        list.Add(order);
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
    }
}
