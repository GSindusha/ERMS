using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessMicroservice.Database.Models;
using Microsoft.EntityFrameworkCore;
using AccessMicroservice.Repositories;



namespace AccessMicroservice.Database
{
    public class DatabaseContext : DbContext
    {
      
     
       public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Users> User { get; set; } = default!;


    }
}
