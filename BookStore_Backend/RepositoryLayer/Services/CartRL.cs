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
    public class CartRL : ICartRL
    {
        private readonly string connectionString;
        public CartRL(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStoreApp");
        }
        public bool AddBookToCart(int UserId, CartDataModel postModel)
        {
            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlconnection)
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand("AddBookToCartSP", sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@BookId", postModel.BookId);
                    cmd.Parameters.AddWithValue("@BookQuantity", postModel.BookQuantity);
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
        public List<CartModel> GetAllBooksInCart(int UserId)
        {
            List<CartModel> list = new List<CartModel>();
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("GetAllBooksInCartSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CartModel book = new CartModel();
                        book.CartId = reader["CartId"] == DBNull.Value ? default : reader.GetInt32("CartId");
                        book.UserId = UserId;
                        book.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        book.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                        book.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                        book.BookQuantity = reader["BookQuantity"] == DBNull.Value ? default : reader.GetInt32("BookQuantity");
                        book.Price = reader["Price"] == DBNull.Value ? default : reader.GetInt32("Price");
                        book.DiscountPrice = reader["DiscountPrice"] == DBNull.Value ? default : reader.GetInt32("DiscountPrice");
                        book.BookImage = reader["BookImage"] == DBNull.Value ? default : reader.GetString("BookImage");
                        list.Add(book);
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
        public bool UpdateCart(int UserId, CartUpdateModel cartUpdateModel)
        {
            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            try
            {
                {
                    sqlconnection.Open();

                    SqlCommand cmd = new SqlCommand("UpdateCartItemSP", sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@CartId", cartUpdateModel.CartId);
                    cmd.Parameters.AddWithValue("@BookId", cartUpdateModel.BookId);
                    cmd.Parameters.AddWithValue("@BookQuantity", cartUpdateModel.BookQuantity);

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
        public bool DeleteCartItembyBookId(int UserId, int CartId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("DeleteCartItemSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CartId ", CartId);
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
        public CartModel GetCartItemByCartId(int CartId, int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("GetCartItemByCartIdSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@CartId", CartId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    CartModel book = new CartModel();
                    if (reader.Read())
                    {
                        book.CartId = reader["CartId"] == DBNull.Value ? default : reader.GetInt32("CartId");
                        book.UserId = UserId;
                        book.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        book.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                        book.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                        book.BookQuantity = reader["BookQuantity"] == DBNull.Value ? default : reader.GetInt32("BookQuantity");
                        book.Price = reader["Price"] == DBNull.Value ? default : reader.GetInt32("Price");
                        book.DiscountPrice = reader["DiscountPrice"] == DBNull.Value ? default : reader.GetInt32("DiscountPrice");
                        book.BookImage = reader["BookImg"] == DBNull.Value ? default : reader.GetString("BookImage");
                    }
                    if (book.BookId == 0)
                    {
                        return null;
                    }
                    return book;
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
