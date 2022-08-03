using Microsoft.AspNetCore.Mvc;
using AccessMicroservice.Database;
using AccessMicroservice.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Transactions;
using AccessMicroservice.Repositories;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccessMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly DatabaseContext _db;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepo;

        public UserController(DatabaseContext db, IConfiguration config,IUserRepository userRepo)
        {
            this._db = db;
            this._config = config;
            this._userRepo = userRepo;

        }
        // GET: api/<Users>
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            //return db.User.ToList();
            var EmployeeList = _userRepo.GetUsers();
            return EmployeeList;

        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(Userdto request)
        {
            await this._userRepo.AddUserAsync(request);
            return Ok("User Created!!");
        }


        //[HttpGet("{id}"), Authorize]
     //   public async Task<ActionResult<Users>> getCurrentUser(string id)
    //    {

       //     List<Users> user = await _db.User.Where(o => o.EmailId == id).ToListAsync();

        //    var userData = await _userRepo.GetUserByIdAsync(id);
         //   return Ok(user[0]);
        //}

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(Userlogin request)
        {
            List<Users> user = await _db.User.Where(o => o.EmailId == request.EmailId).ToListAsync();
            if (user[0].EmailId != request.EmailId)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user[0].PasswordHash, user[0].PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user[0]);

            return Ok(token);
        }

        // Util Methods
        private string CreateToken(Users user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.EmailId),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }






    }
}
