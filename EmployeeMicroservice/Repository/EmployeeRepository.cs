using EmployeeMicroservice.Controllers;
using EmployeeMicroservice.Database;
using EmployeeMicroservice.Database.Entities;
using EmployeeMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace EmployeeMicroservice.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        //   private readonly DatabaseContext db;

        // public EmployeeRepository(DatabaseContext db)
        //  {
        //      _db = db;
        //    }
        Database.DatabaseContext db = default!;
        public EmployeeRepository()
        {
            db = new DatabaseContext();

        }
       

        public IEnumerable<Employee> GetEmployees()
        {
            //throw new NotImplementedException();
            return db.Employee.ToList();
        }

        public Employee GetEmployeebyId(int id)
        {
            //throw new NotImplementedException();
             return db.Employee.Find(id);

            //var emp = db.Employee.Find(id);
            //if (emp == null) throw new KeyNotFoundException("Employee not found");
           // return emp;

            //return GetEmployeebyId(id);
        }

            public void AddEmployee(Employee employee)
        {
            db.Add(employee);
            save();
        }

        public void DeleteEmployee(int id)
        {
            var employee=  db.Employee.Find(id);
            //var employee = new Employee();
            db.Employee.Remove(employee);
            save();
        }

        public void save()
        {
            db.SaveChanges();
        }

        //  public void UpdateProduct(Employee employee)
        //  {
        //      _dbContext.Entry(employee).State = EntityState.Modified;
        // Save();
        //     }

       // private Employee GetEmployeebyId(int id)
       // { }


    }
}
