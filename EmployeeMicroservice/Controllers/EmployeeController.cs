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

        private readonly IConfiguration _configuration=default!;
        public EmployeeController(IConfiguration configuration)
        {
            
            this._configuration = configuration;
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


        [HttpPost]
        [Route("Registration")]
        public async Task<ActionResult<string>> Registration(Employee request)
        {

            var employee = new Employee();

            employee.Name = request.Name;
            employee.Gender = request.Gender;
            employee.Age = request.Age;
            employee.Salary = request.Salary;
            employee.Phone = request.Phone;
            employee.EmailId = request.EmailId;
            employee.Password = request.Password;
            employee.Type = request.Type;


            db.Employee.Add(employee);
            await db.SaveChangesAsync();
            return Ok("User Created!!");
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(Employee employee)
        {
            List<Employee> user = await db.Employee.Where(o => o.EmailId == employee.EmailId).ToListAsync();
            if (user[0].EmailId != employee.EmailId)
            {
                return BadRequest("User not found.");
            }

            string token = CreateToken(user[0]);

            return Ok(token);
        }

        private string CreateToken(Employee employee)
        {
            // throw new NotImplementedException();
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, employee.EmailId),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }

        //Post method - ResetPassword

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

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]    //no update feature in ui
        //public void Put(int id, [FromBody] string value)
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee employee)
        {
            var dbEmployee= await db.Employee.FindAsync(employee.EmployeeId);
            if (dbEmployee == null) { return BadRequest("Employee Not Found"); }

       
            dbEmployee.Name = employee.Name;
            dbEmployee.Gender = employee.Gender;
            dbEmployee.Age = employee.Age;
            dbEmployee.Salary = employee.Salary;
            dbEmployee.Phone = employee.Phone;
            dbEmployee.EmailId = employee.EmailId;
            dbEmployee.Password = employee.Password;
            dbEmployee.Type = employee.Type;


            await db.SaveChangesAsync();
            return Ok(await db.Employee.ToListAsync());

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
