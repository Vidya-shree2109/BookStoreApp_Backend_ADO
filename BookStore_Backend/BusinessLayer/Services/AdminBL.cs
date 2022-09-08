using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public string AdminLogin(AdminModel adminModel)
        {
            try
            {
                return this.adminRL.AdminLogin(adminModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
