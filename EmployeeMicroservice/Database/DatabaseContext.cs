using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMicroservice.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMicroservice.Database
{
    public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        //public Microsoft.EntityFrameworkCore.DbSet<Entities.Employee> Employee { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Employee> Employee { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=LAPTOP-OHKIT41O\SQLEXPRESS; database = EMS2; Integrated Security = true");

        }
    }
}
