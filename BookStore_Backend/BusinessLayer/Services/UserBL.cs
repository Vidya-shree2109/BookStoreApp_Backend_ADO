using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public UserRegistrationModel UserRegistration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                return this.userRL.UserRegistration(userRegistrationModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string UserLogin(UserLoginModel userLoginModel)
        {
            try
            {
                return this.userRL.UserLogin(userLoginModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
