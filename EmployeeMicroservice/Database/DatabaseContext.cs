using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMicroservice.Database.Entities;
using Microsoft.EntityFrameworkCore;
using EmployeeMicroservice.Repository;

namespace EmployeeMicroservice.Database
{
    public class DatabaseContext : DbContext
    {
        //public Microsoft.EntityFrameworkCore.DbSet<Entities.Employee> Employee { get; set; }
        public DbSet<Employee> Employee { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=LAPTOP-OHKIT41O\SQLEXPRESS; database = EMS2; Integrated Security = true");

        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
