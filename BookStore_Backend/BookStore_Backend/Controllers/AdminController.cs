using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IAdminBL adminBL;
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost("Login")]
        public IActionResult AdminLogin(AdminModel adminModel)
        {
            try
            {
                var result = this.adminBL.AdminLogin(adminModel);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Admin Login Successful.. !", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Admin Login failed.. !!" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
