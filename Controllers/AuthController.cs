//using AMO_4.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace AMO_4.Controllers
//{
//    [Route("api/auth")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
        

//        // POST api/<AuthController>
//        [HttpPost, Route("essai")]
//        public IActionResult Login([FromBody] LoginModel user)
//        {
//            if (user is null)
//            {
//                return BadRequest("Invalid client request");
//            }
//            if (user.UserName == "johndoe" && user.Password == "def@123")
//            {
//                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
//                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
//                var claims = new List<Claim>
//                {
//                    new Claim(ClaimTypes.Name, user.UserName),
//                    new Claim(ClaimTypes.Role, "visiteur")
//                };
//                var tokeOptions = new JwtSecurityToken(
//                    issuer: "https://localhost:5001",
//                    audience: "https://localhost:5001",
//                    claims: claims,
//                    expires: DateTime.Now.AddMinutes(5),
//                    signingCredentials: signinCredentials
//                );
//                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
//                return Ok(new AuthenticatedResponse { Token = tokenString });
//            }
//            return Unauthorized();
//        }
//    }

   
    
//}
