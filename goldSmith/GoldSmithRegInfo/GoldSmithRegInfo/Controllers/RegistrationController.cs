using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoldSmithRegInfo.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace GoldSmithRegInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private UserManager<goldSmithUsers> _usermanager;
        private SignInManager<goldSmithUsers> _signinmanager;
        private readonly AppSettings _appsettings;
        public RegistrationController(UserManager<goldSmithUsers> usermanager, SignInManager<goldSmithUsers> signinmanager,IOptions<AppSettings> appsettings)
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
            _appsettings = appsettings.Value;
        }
        [HttpPost]
        [Route("reg")]
        public async Task<object> PostReg(GoldSmithInfoModel model)
        {
            var smith = new goldSmithUsers()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ShopName = model.ShopName,
                Image = model.Image,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                LicensNumber = model.LicensNumber,
                Email = model.Email,
                Active = false,
            };
            try
            {
                var Result = await _usermanager.CreateAsync(smith, model.Password);
                return Ok(Result);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var smith = await _usermanager.FindByNameAsync(model.UserName);
            if (smith!=null && await _usermanager.CheckPasswordAsync(smith, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("userID",smith.Id.ToString())
                    }),
                    Expires=DateTime.UtcNow.AddDays(1),
                    SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appsettings.JWT_Secret)),SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });    
            }
            else
            {
                return BadRequest(new { message = "Username Or Password Is Incorrect" });
            }
        }
    }

}