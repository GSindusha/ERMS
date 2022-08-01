using System.Collections.Generic;
using EmployeeMicroservice.Database.Entities;

namespace EmployeeMicroservice.Repository
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeebyId(int id);
        void AddEmployee(Employee employee);
        void DeleteEmployee(int id);
        void save();


    }
}
