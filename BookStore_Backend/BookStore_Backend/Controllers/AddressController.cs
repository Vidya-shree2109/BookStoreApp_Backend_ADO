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
    [Authorize(Roles = Role.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        IAddressBL addressBL;

        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [HttpPost("AddAddress")]
        public IActionResult AddAddress(AddressPostModel postModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = this.addressBL.AddAddress(UserId, postModel);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Someting Went Wrong While adding Address for UserId : {UserId} !! Check if Address is already availbale in Addresses!!" });
                }

                return this.Ok(new { success = true, Message = $"Address For UserId : {UserId} Added Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAllAddress")]
        public IActionResult GetAllAddress()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                List<AddressResponseModel> result = this.addressBL.GetAllAddress(UserId);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = $"No Addresses available For UserId : {UserId}!!" });
                }

                return this.Ok(new { success = true, Message = $"Addresses List of UserId : {UserId} fetched Sucessfully...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAddressById/{AddressId}")]
        public IActionResult GetAddressById(int AddressId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = this.addressBL.GetAddressById(AddressId, UserId);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = $"No AddressesId : {AddressId} available For UserId : {UserId}!!" });
                }

                return this.Ok(new { success = true, Message = $"AddressesId : {UserId} details fetched Sucessfully...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("UpdateAddress")]
        public IActionResult UpdateAddressbyId(AddressPutModel postModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = this.addressBL.UpdateAddressbyId(UserId, postModel);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Update Address Failed!! Check if Address is already availbale..." });
                }

                return this.Ok(new { success = true, Message = $"AddressId : {postModel.AddressId} Updated Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("DeleteAddress/{AddressId}")]
        public IActionResult DeleteAddressByAddressId(int AddressId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = this.addressBL.DeleteAddressByAddressId(AddressId, UserId);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Something went wrong while deleting Address of AddressId : {AddressId}!!" });
                }

                return this.Ok(new { success = true, Message = $"AddressId : {AddressId} Deleted Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
