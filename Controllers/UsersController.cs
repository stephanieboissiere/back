using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AMO_4.Data;
using AMO_4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Security.Cryptography;
using AMO_4.Services;

namespace AMO_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyWebApiContext _context;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public UsersController(MyWebApiContext context, IConfiguration configuration, ITokenService tokenService)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        // GET: api/Users
        [HttpGet, Authorize(Roles="admin")]
        //
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.Include(b => b.Role).ToListAsync();
        }
        
        // GET: api/Users/5
        [HttpGet("{id}"), Authorize(Roles="admin")]
        
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.Include(b => b.Role).FirstOrDefaultAsync(i => i.userId == id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
        //GET:api/Users/CurrentUser
        [HttpGet, Authorize, Route("CurrentUser")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            var user = await _context.Users.Include(b => b.Role).FirstOrDefaultAsync(i => i.username == User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        //public async Task<ActionResult<User>> GetCurrentUser()
        //{
        //    var user = await _context.Users.Include(b => b.Role).FirstOrDefaultAsync(i => i.username == User.Identity.Name);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return user;
        //}

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize(Roles = "admin")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.userId)
            {
                return BadRequest();
            }
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] User userModel)
        {
            if (userModel is null)
            {
                return BadRequest("Invalid client request");
            }
            string password = PasswordService.hashPassword(userModel.password);
            var user = _context.Users.Include(b => b.Role).FirstOrDefault(u =>(u.username == userModel.username) && (u.password == password));
            if (user is null)
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userModel.username),
                new Claim(ClaimTypes.Role, user.Role.First().name)
            };
            //contient la logique pour générer le jeton d'accès
            var accessToken = _tokenService.GenerateAccessToken(claims);
            //contient la logique pour générer le jeton d'actualisation.
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);

            _context.SaveChanges();

            return Ok(new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }
        //if (user != null)//
        //{
        //    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:token").Value));//
        //    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);//
        //    var claims = new List<Claim>();
        //    try { 
        //     claims = new List<Claim>
        //    {
        //    new Claim(ClaimTypes.Name, user.username),
        //    new Claim(ClaimTypes.Role, user.Role.First().name),

        //    }; 
        //    }catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    var tokeOptions = new JwtSecurityToken(
        //               issuer: "https://localhost:5001",
        //               audience: "https://localhost:5001",
        //               claims: claims,
        //               expires: DateTime.Now.AddMinutes(50),
        //               signingCredentials: signinCredentials);

        //    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

        //   return Ok(new AuthenticatedResponse { 
        //        Token = tokenString,
        //        name = user.username,
        //        //role = user.Role.First().name,

        //    });
        //} else
        //{
        //    return NotFound();
        //}




        [HttpPost, Authorize(Roles = "admin")]
        public async Task<ActionResult<User>> PostUser(User user)
        {

            var dbuser = _context.Users.Where(u => u.username == user.username).FirstOrDefault();
            if (dbuser != null)
            {
                return BadRequest("Username already exists in database");
            }
            user.password = PasswordService.hashPassword(user.password);
           
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.userId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.userId == id);
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        //[HttpGet, Route("User")]
        //public IActionResult Get()
        //{
        //    return Ok(new AuthenticatedResponse
        //    {

        //        name = User.Identity.Name,
        //        role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,

        //      
        //    });

        //}
    }
}
