using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public UserRegistrationModel UserRegistration(UserRegistrationModel userRegistrationModel);
        public string UserLogin(UserLoginModel userLoginModel);

    }
}
