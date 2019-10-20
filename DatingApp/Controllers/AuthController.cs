using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DatingApp.Data;
using DatingApp.Models;
using DatingApp.Dtos;
using net.openstack.Providers.Rackspace.Objects.Databases;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using DatingApp.Helpers;

namespace DatingApp.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    // [EnableCors("EnableCORS")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config )
        {
            _repo = repo;
            _config = config;
        }
        // POST: api/Auth
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            if (await _repo.UserExist(userForRegisterDto.UserName))
            {
                return BadRequest("Username Already Exist");
            }
            var UserTOcreate = new Users
            {
                Name = userForRegisterDto.UserName
            };
            await _repo.Rigster(UserTOcreate, userForRegisterDto.Password);
            
            return StatusCode(201);
        }

        // PUT: api/Auth/5
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserForLogInDto userForLoGInDto)
        {

            var userFromRepo = await _repo.LogIn(userForLoGInDto.UserName, userForLoGInDto.Password);
            if (userFromRepo == null)
            {
               return BadRequest(BadRequstErrors.UserPasswordIncorrect);
            }
            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name , userFromRepo.Name)

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSetting:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(
                new
                {
                   token = tokenHandler.WriteToken(token)
                }
                );
        }


    }
}
