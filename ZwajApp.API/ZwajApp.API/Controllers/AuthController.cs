using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ZwajApp.API.Data;
using ZwajApp.API.Dtos;
using ZwajApp.API.Models;

namespace ZwajApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo,IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //validation
            //if (!ModelState.IsValid)
                //return BadRequest(ModelState);
            userForRegisterDto.username = userForRegisterDto.username.ToLower();
            if (await _repo.UserExists(userForRegisterDto.username))
            {
                return BadRequest("هذا المستخدم مسجل من قبل");
            }
            var userToCreate = new User
            {
                UserName = userForRegisterDto.username
            };
            var CreatedUser = _repo.Register(userToCreate, userForRegisterDto.password);
            return StatusCode(201);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            
                var userFromRepo = await _repo.Login(userForLoginDto.username.ToLower(), userForLoginDto.password);
                if (userFromRepo == null)
                    return Unauthorized();
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.UserName)
                };
                //  الي بعطي التكون هوا السيرفر  هوا الي بولدها وبتكون مخزنة على السيرفر ا 
                //تعني المطالبة بطلب السيرفر المعطيات تعت التوكن Claim 
                // بالتالي التوكن مهمته انو السيرفر مش حيروح يجيب البيانات من قاعدة البيانات حيجيبهم من التوكن 

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                //شهادات بتكون موجودة على السيرفر فيها بينانات مشففرة مشان لما يتحقق ميروحش على الداتا بيز 

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
                // المتغير الي راح يشيل التوكن
                var tokenDescripror = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };
                //هوا الي حيستلم الدسكتربتر وحياخد منه التوكن
                var TokenHandler = new JwtSecurityTokenHandler();
                // تسليم السلبتور
                var token = TokenHandler.CreateToken(tokenDescripror);
                return Ok(new
                {
                    token = TokenHandler.WriteToken(token)
                }); ;
            

          
        }
    }
}