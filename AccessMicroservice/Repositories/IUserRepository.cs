using System.Collections.Generic;
using AccessMicroservice.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccessMicroservice.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<Users> GetUsers();
        Task<List<Users>> GetUserByIdAsync(string ID);
        Task<ActionResult<string>> AddUserAsync(Userdto UserData);

    }
}
