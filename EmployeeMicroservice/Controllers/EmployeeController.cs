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
using System.Transactions;
using EmployeeMicroservice.Repository;



//using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeMicroservice.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
      
        private readonly IEmployeeRepository _employeeRepository = default!;
     
       
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var EmployeeList = _employeeRepository.GetEmployees();
            return Ok(EmployeeList);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {

            var Employee = _employeeRepository.GetEmployeebyId(id);
             return Ok(Employee);
            
            
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            using (var scope = new TransactionScope())
            {
                _employeeRepository.AddEmployee(employee);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = employee.EmployeeId }, employee);
            }
        }

       

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _employeeRepository.DeleteEmployee(id);
            return new OkResult();
        }
        
        
    }
    
}
