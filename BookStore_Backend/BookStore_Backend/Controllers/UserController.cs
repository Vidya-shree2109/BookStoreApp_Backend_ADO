using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("Register")]
        public IActionResult UserRegistration(UserRegistrationModel registrationModel)
        {
            try
            {
                var result = this.userBL.UserRegistration(registrationModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "User Registration Sucessfull !!", data = result });
                }
                return this.BadRequest(new { success = false, Message = "User Registration Failed.. !" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("Login")]
        public IActionResult UserLogin(UserLoginModel userLoginModel)
        {
            try
            {
                var result = this.userBL.UserLogin(userLoginModel);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Login Successful.. !", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Login failed.. !!" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
