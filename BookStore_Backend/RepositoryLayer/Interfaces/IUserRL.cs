using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public UserRegistrationModel UserRegistration(UserRegistrationModel userRegistrationModel);
        public string UserLogin(UserLoginModel userLoginModel);
        public bool UserForgotPassword(string EmailId);
        public bool UserResetPassword(string EmailId, UserPasswordModel userPasswordModel);

    }
}
