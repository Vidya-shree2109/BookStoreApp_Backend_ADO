using CommonLayer.Model;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration configuration;

        public UserRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public UserRegistrationModel UserRegistration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(configuration["ConnectionStrings:BookStoreApp"]))
                {
                    SqlCommand cmd = new SqlCommand("UserRegistrationSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FullName", userRegistrationModel.FullName);
                    cmd.Parameters.AddWithValue("@EmailId", userRegistrationModel.EmailId);
                    cmd.Parameters.AddWithValue("@Password", userRegistrationModel.Password);
                    cmd.Parameters.AddWithValue("@Phone", userRegistrationModel.Phone);

                    sqlConnection.Open();
                    var result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();

                    if (result != 0)
                    {
                        return userRegistrationModel;
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
        }
        public string UserLogin(UserLoginModel userLoginModel)
        {
            SqlConnection sqlConnection = new SqlConnection(configuration["ConnectionStrings:BookStoreApp"]);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("UserLoginSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", userLoginModel.EmailId);
                    cmd.Parameters.AddWithValue("@Password", userLoginModel.Password);
                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();
                    GetAllUsersModel response = new GetAllUsersModel();
                    if (reader.Read())
                    {
                        response.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        response.EmailId = reader["EmailId"] == DBNull.Value ? default : reader.GetString("EmailId");
                        response.Password = reader["Password"] == DBNull.Value ? default : reader.GetString("Password");
                    }
                    return GenerateJWTSecurityToken(response.EmailId, response.UserId);
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

        private string GenerateJWTSecurityToken(string emailId, int userId)
        {
            try
            {
                // generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("EmailId", emailId),
                    new Claim("UserId",userId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),

                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string EncryptPassword(string Password)
        {
            try
            {
                if (Password == null)
                {
                    return null;
                }
                else
                {
                    byte[] b = Encoding.ASCII.GetBytes(Password);
                    string encrypted = Convert.ToBase64String(b);
                    return encrypted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string DecryptedPassword(string encryptedPassword)
        {
            byte[] b;
            string decrypted;
            try
            {
                if (encryptedPassword == null)
                {
                    return null;
                }
                else
                {
                    b = Convert.FromBase64String(encryptedPassword);
                    decrypted = Encoding.ASCII.GetString(b);
                    return decrypted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UserForgotPassword(string EmailId)
        {
            SqlConnection sqlConnection = new SqlConnection(configuration["ConnectionStrings:BookStoreApp"]);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("UserForgotPasswordSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", EmailId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    GetAllUsersModel response = new GetAllUsersModel();
                    if (reader.Read())
                    {
                        response.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        response.EmailId = reader["EmailId"] == DBNull.Value ? default : reader.GetString("EmailId");
                        response.FullName = reader["FullName"] == DBNull.Value ? default : reader.GetString("FullName");
                    }

                    if (response.UserId == null || response.FullName == null || response.EmailId == null)
                    {
                        return false;
                    }

                    MessageQueue messageQueue;
                    //add message to queue
                    if (MessageQueue.Exists(@".\private$\BookStoreQueue"))
                    {
                        messageQueue = new MessageQueue(@".\private$\BookStoreQueue");
                    }
                    else
                    {
                        messageQueue = MessageQueue.Create(@".\private$\BookStoreQueue");
                    }

                    Message Mymessage = new Message();
                    Mymessage.Formatter = new BinaryMessageFormatter();
                    Mymessage.Body = this.GenerateToken(EmailId);
                    Mymessage.Label = "Forgot Password Email";
                    messageQueue.Send(Mymessage);

                    Message msg = messageQueue.Receive();
                    msg.Formatter = new BinaryMessageFormatter();
                    EmailService.SendEmail(EmailId, msg.Body.ToString(), response.FullName);
                    messageQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(MsmqQueue_ReceiveCompleted);
                    messageQueue.BeginReceive();
                    messageQueue.Close();

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
        private void MsmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()), e.Message.ToString());
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode == MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access Denied!!" + "Queue might be system queue...");
                }
            }
        }
        private string GenerateToken(string email)
        {
            if (email == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", email)
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
