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
    public class BookRL : IBookRL
    {
        private readonly string connectionString;

        public BookRL(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStoreApp");
        }

        public BookPostModel AddBook(BookPostModel bookPostModel)
        {
            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            try
            {
                {
                    sqlconnection.Open();

                    SqlCommand cmd = new SqlCommand("AddBookSP", sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookName ", bookPostModel.BookName);
                    cmd.Parameters.AddWithValue("@Author", bookPostModel.Author);
                    cmd.Parameters.AddWithValue("@Description ", bookPostModel.Description);
                    cmd.Parameters.AddWithValue("@Quantity", bookPostModel.Quantity);
                    cmd.Parameters.AddWithValue("@Price", bookPostModel.Price);
                    cmd.Parameters.AddWithValue("@DiscountPrice ", bookPostModel.DiscountPrice);
                    cmd.Parameters.AddWithValue("@TotalRating ", bookPostModel.TotalRating);
                    cmd.Parameters.AddWithValue("@RatingCount", bookPostModel.RatingCount);
                    cmd.Parameters.AddWithValue("@BookImg", bookPostModel.BookImg);

                    var result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        return bookPostModel;
                    }
                    else
                    {
                        return null;
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
        public List<BookResponseModel> GetAllBooks()
        {
            List<BookResponseModel> listOfUsers = new List<BookResponseModel>();
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("GetAllBooksSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        BookResponseModel book = new BookResponseModel();
                        book.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        book.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                        book.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                        book.Description = reader["Description"] == DBNull.Value ? default : reader.GetString("Description");
                        book.Quantity = reader["Quantity"] == DBNull.Value ? default : reader.GetInt32("Quantity");
                        book.Price = reader["Price"] == DBNull.Value ? default : reader.GetDecimal("Price");
                        book.DiscountPrice = reader["DiscountPrice"] == DBNull.Value ? default : reader.GetDecimal("DiscountPrice");
                        book.TotalRating = reader["TotalRating"] == DBNull.Value ? default : reader.GetDouble("TotalRating");
                        book.RatingCount = reader["RatingCount"] == DBNull.Value ? default : reader.GetInt32("RatingCount");
                        book.BookImg = reader["BookImg"] == DBNull.Value ? default : reader.GetString("BookImg");
                        listOfUsers.Add(book);
                    }

                    return listOfUsers;
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
        public BookResponseModel GetBookById(int BookId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("GetBooksByIdSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId ", BookId);
                    var result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        return null;
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    BookResponseModel book = new BookResponseModel();
                    while (reader.Read())
                    {
                        book.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        book.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                        book.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                        book.Description = reader["Description"] == DBNull.Value ? default : reader.GetString("Description");
                        book.Quantity = reader["Quantity"] == DBNull.Value ? default : reader.GetInt32("Quantity");
                        book.Price = reader["Price"] == DBNull.Value ? default : reader.GetDecimal("Price");
                        book.DiscountPrice = reader["DiscountPrice"] == DBNull.Value ? default : reader.GetDecimal("DiscountPrice");
                        book.TotalRating = reader["TotalRating"] == DBNull.Value ? default : reader.GetDouble("TotalRating");
                        book.RatingCount = reader["RatingCount"] == DBNull.Value ? default : reader.GetInt32("RatingCount");
                        book.BookImg = reader["BookImg"] == DBNull.Value ? default : reader.GetString("BookImg");
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

        public BookResponseModel UpdateBooks(int BookId, BookPostModel bookPostModel)
        {

            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            try
            {
                {
                    sqlconnection.Open();

                    SqlCommand cmd = new SqlCommand("UpdateBooksSP", sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    cmd.Parameters.AddWithValue("@BookName ", bookPostModel.BookName);
                    cmd.Parameters.AddWithValue("@Author", bookPostModel.Author);
                    cmd.Parameters.AddWithValue("@Description ", bookPostModel.Description);
                    cmd.Parameters.AddWithValue("@Quantity", bookPostModel.Quantity);
                    cmd.Parameters.AddWithValue("@Price", bookPostModel.Price);
                    cmd.Parameters.AddWithValue("@DiscountPrice ", bookPostModel.DiscountPrice);
                    cmd.Parameters.AddWithValue("@TotalRating ", bookPostModel.TotalRating);
                    cmd.Parameters.AddWithValue("@RatingCount", bookPostModel.RatingCount);
                    cmd.Parameters.AddWithValue("@BookImg", bookPostModel.BookImg);

                    var result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        BookResponseModel response = new BookResponseModel();
                        if (reader.Read())
                        {
                            response.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                            response.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                            response.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                            response.Description = reader["Description"] == DBNull.Value ? default : reader.GetString("Description");
                            response.Quantity = reader["Quantity"] == DBNull.Value ? default : reader.GetInt32("Quantity");
                            response.Price = reader["Price"] == DBNull.Value ? default : reader.GetDecimal("Price");
                            response.DiscountPrice = reader["DiscountPrice"] == DBNull.Value ? default : reader.GetDecimal("DiscountPrice");
                            response.TotalRating = reader["TotalRating"] == DBNull.Value ? default : reader.GetDouble("TotalRating");
                            response.RatingCount = reader["RatingCount"] == DBNull.Value ? default : reader.GetInt32("RatingCount");
                            response.BookImg = reader["BookImg"] == DBNull.Value ? default : reader.GetString("BookImg");
                        }

                        return response;
                    }
                    else
                    {
                        return null;
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
        public bool DeleteBook(int BookId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("deleteBookSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId ", BookId);
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

    }
}
