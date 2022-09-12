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
    public class FeedbackRL : IFeedbackRL
    {
        private readonly string connectionString;

        public FeedbackRL(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStoreApp");
        }

        public bool AddFeedback(int UserId, FeedbackPostModel postModel)
        {
            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlconnection)
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand("AddFeedbackSP", sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@BookId", postModel.BookId);
                    cmd.Parameters.AddWithValue("@Rating", postModel.Rating);
                    cmd.Parameters.AddWithValue("@Comment", postModel.Comment);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 1)
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

        public List<FeedbackResponseModel> GetAllFeedbacksByBookId(int BookId)
        {
            List<FeedbackResponseModel> list = new List<FeedbackResponseModel>();
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("GetFeedbackByBookId", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        FeedbackResponseModel Feedbackdetails = new FeedbackResponseModel();
                        Feedbackdetails.FeedbackId = reader["FeedbackId"] == DBNull.Value ? default : reader.GetInt32("FeedbackId");
                        Feedbackdetails.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        Feedbackdetails.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        Feedbackdetails.Rating = reader["Rating"] == DBNull.Value ? default : reader.GetDouble("Rating");
                        Feedbackdetails.Comment = reader["Comment"] == DBNull.Value ? default : reader.GetString("Comment");
                        Feedbackdetails.FullName = reader["FullName"] == DBNull.Value ? default : reader.GetString("FullName");
                        list.Add(Feedbackdetails);
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

        public bool DeleteFeedbackById(int FeedbackId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("DeleteFeedbackByIdSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FeedbackId ", FeedbackId);

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
