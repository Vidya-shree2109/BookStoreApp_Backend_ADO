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
        public BookRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("BookStoreApp");
        }

        public BookModel AddBook(BookModel bookModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("AddBookSP", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    com.Parameters.AddWithValue("@Author", bookModel.Author);
                    com.Parameters.AddWithValue("@Description", bookModel.Description);
                    com.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                    com.Parameters.AddWithValue("@Price", bookModel.Price);
                    com.Parameters.AddWithValue("@DiscountPrice", bookModel.DiscountPrice);
                    com.Parameters.AddWithValue("@TotalRating", bookModel.TotalRating);
                    com.Parameters.AddWithValue("@RatingCount", bookModel.RatingCount);
                    com.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                    var result = com.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return bookModel;
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
                connection.Close();
            }
        }
        public List<BookModel> GetAllBooks()
        {
            List<BookModel> listOfBooks = new List<BookModel>();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("GetAllBooksSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    BookModel getAllBook = new BookModel();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        getAllBook.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        getAllBook.BookName = Convert.ToString(reader["BookName"]);
                        getAllBook.Author = Convert.ToString(reader["Author"]);
                        getAllBook.Description = Convert.ToString(reader["Description"]);
                        getAllBook.Quantity = Convert.ToInt32(reader["Quantity"]);
                        getAllBook.Price = Convert.ToInt32(reader["Price"]);
                        getAllBook.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                        getAllBook.TotalRating = Convert.ToDouble(reader["TotalRating"]);
                        getAllBook.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                        getAllBook.BookImage = Convert.ToString(reader["BookImage"]);

                        listOfBooks.Add(getAllBook);
                    }
                    return listOfBooks;
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
        public BookModel GetBookById(int BookId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("GetBookByIdSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId ", BookId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    BookModel getBookById = new BookModel();
                    while (reader.Read())
                    {
                        getBookById.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        getBookById.BookName = Convert.ToString(reader["BookName"]);
                        getBookById.Author = Convert.ToString(reader["Author"]);
                        getBookById.Description = Convert.ToString(reader["Description"]);
                        getBookById.Quantity = Convert.ToInt32(reader["Quantity"]);
                        getBookById.Price = Convert.ToInt32(reader["Price"]);
                        getBookById.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                        getBookById.TotalRating = Convert.ToDouble(reader["TotalRating"]);
                        getBookById.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                        getBookById.BookImage = Convert.ToString(reader["BookImage"]);
                    }
                    if (getBookById.BookId > 0)
                    {
                        return getBookById;
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
                sqlConnection.Close();
            }
        }
        public BookModel UpdateBook(int BookId, BookModel bookModel)
        {

            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            try
            {
                {
                    sqlconnection.Open();

                    SqlCommand com = new SqlCommand("UpdateBookSP", sqlconnection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@BookId", BookId);
                    com.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    com.Parameters.AddWithValue("@Author", bookModel.Author);
                    com.Parameters.AddWithValue("@Description", bookModel.Description);
                    com.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                    com.Parameters.AddWithValue("@Price", bookModel.Price);
                    com.Parameters.AddWithValue("@DiscountPrice", bookModel.DiscountPrice);
                    com.Parameters.AddWithValue("@TotalRating", bookModel.TotalRating);
                    com.Parameters.AddWithValue("@RatingCount", bookModel.RatingCount);
                    com.Parameters.AddWithValue("@BookImage", bookModel.BookImage);

                    var result = com.ExecuteNonQuery();
                    if (result != 0)
                    {
                        SqlDataReader reader = com.ExecuteReader();
                        BookModel response = new BookModel();
                        if (reader.Read())
                        {
                            response.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                            response.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                            response.Author = reader["AuthorName"] == DBNull.Value ? default : reader.GetString("Author");
                            response.Description = reader["Description"] == DBNull.Value ? default : reader.GetString("Description");
                            response.Quantity = reader["Quantity"] == DBNull.Value ? default : reader.GetInt32("Quantity");
                            response.Price = reader["OriginalPrice"] == DBNull.Value ? default : reader.GetInt32("Price");
                            response.DiscountPrice = reader["DiscountPrice"] == DBNull.Value ? default : reader.GetInt32("DiscountPrice");
                            response.TotalRating = reader["AvgRating"] == DBNull.Value ? default : reader.GetDouble("TotalRating");
                            response.RatingCount = reader["RatingCount"] == DBNull.Value ? default : reader.GetInt32("RatingCount");
                            response.BookImage = reader["BookImg"] == DBNull.Value ? default : reader.GetString("BookImage");
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
                    SqlCommand cmd = new SqlCommand("DeleteBookSP", sqlConnection);
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
