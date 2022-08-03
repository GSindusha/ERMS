using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AccessMicroservice.Database;
using AccessMicroservice.Database.Models;
using AccessMicroservice.Controllers;
using AccessMicroservice.Repositories;

namespace AccessMicroservice.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _db;
        private readonly IConfiguration _config;
        public UserRepository(DatabaseContext db, IConfiguration config)
        {
            this._db = db;
            this._config = config;
            
        }
        public async Task<ActionResult<string>> AddUserAsync(Userdto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new Users();

            user.Name = request.Name;
            user.EmailId = request.EmailId;
            user.Gender = request.Gender;
            user.Age = request.Age;
            user.Salary = request.Salary;
            user.Phone = request.Phone;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _db.User.Add(user);
            await _db.SaveChangesAsync();
            return "User Created!!";
        }

        public async Task<List<Users>> GetUserByIdAsync(string id)
        {
            List<Users> user = await _db.User.Where(o => o.EmailId == id).ToListAsync();
            return user;
        }

        public IEnumerable<Users> GetUsers()
        {
            // throw new NotImplementedException();
            return _db.User.ToList();
        }

      

        

        // Util Method
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
