using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Collections.Generic;
using System;
using EmployeeMicroservice.Database;
using EmployeeMicroservice.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


//using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeMicroservice.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        Database.DatabaseContext db=default!;
        public EmployeeController()
        {
            db = new DatabaseContext();

        }

        
        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
          return db.Employee.ToList();  //To fetch the EmployeeList
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]  //fr values
        public Employee Get(int id)
        {
            return db.Employee.Find(id);  //To fetch the info based on the id
        }


       
      

        // POST api/<EmployeeController>
        [HttpPost]
        public IActionResult Post([FromBody] Employee model) //fr values
        {
            try
            {
                db.Employee.Add(model);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, model);       
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex);
            }

        }

       

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        // public void Delete(int id)
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var dbEmployee = await db.Employee.FindAsync(id);
            if (dbEmployee == null) { return BadRequest("Employee Not Found"); }

            db.Employee.Remove(dbEmployee);
            await db.SaveChangesAsync();

            return Ok(await db.Employee.ToListAsync());

        }
    }
    
}
