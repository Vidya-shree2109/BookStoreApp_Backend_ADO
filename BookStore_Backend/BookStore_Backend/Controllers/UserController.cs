using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
        [HttpPost("ForgotPassword")]
        public IActionResult UserForgotPassword(string EmailId)
        {
            try
            {
                if (EmailId == null)
                {
                    return this.BadRequest(new { success = false, Message = "Unsuccessful, Enter valid email address.. !" });
                }
                var result = this.userBL.UserForgotPassword(EmailId);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = "Something went wrong, mail not sent.. !" });
                }

                return this.Ok(new { success = true, Message = $"Mail has sent successfully to reset password" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult UserResetPassword(UserPasswordModel userPasswordModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var emailId = claims.Where(p => p.Type == @"EmailId").FirstOrDefault()?.Value;
                bool result = this.userBL.UserResetPassword(emailId, userPasswordModel);
                if (result == true)
                {
                    return this.Ok(new { success = true, Message = $"Reset Password successful for EmailId : {emailId} !" });
                }

                return this.BadRequest(new { success = false, Message = $"Reset Password Unsuccessful for EmailId : {emailId} !" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
