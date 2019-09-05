using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoldSmithRegInfo.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoldSmithRegInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginInfoController : ControllerBase
    {
        private UserManager<goldSmithUsers> _usermanager;
        public LoginInfoController(UserManager<goldSmithUsers> usermanager)
        {
            _usermanager = usermanager;
        }
        [HttpGet]
        [Authorize]
        // GET : /api/LoginInfo
        public async Task<Object> GetSmithProfile()
        {
            string SmithId = User.Claims.First(c => c.Type == "userID").Value;
            var Smith = await _usermanager.FindByIdAsync(SmithId);
            return new
            {
                Smith.FirstName,
                Smith.LastName,
                Smith.Image,
                Smith.ShopName,
                Smith.PhoneNumber,
                Smith.Address,
                Smith.LicensNumber,
                Smith.Email,
            };
        }
    }
}