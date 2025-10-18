using Microsoft.AspNetCore.Mvc;
using SimpleCrudWebApp.Implementation;
using SimpleCrudWebApp.Interface;
using SimpleCrudWebApp.Models;

namespace SimpleCrudWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepositorycs employeeRepositorycs;
        private readonly OrgDbContext orgDbContext;
        public EmployeeController(IEmployeeRepositorycs employeeRepository, OrgDbContext orgDbContext)
        {
            this.orgDbContext = orgDbContext;
            this.employeeRepositorycs = employeeRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(SimpleCrudWebApp.Write.Employee.Create employee)
        {
            if (employee == null)
            {
                return BadRequest("Invalid employee data.");
            }
            await employeeRepositorycs.CreateAsync(employee);
            await orgDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(SimpleCrudWebApp.Write.Employee.Update employee)
        {
            if (employee == null)
            {
                return BadRequest("Invalid employee data.");
            }
            await employeeRepositorycs.UpdateAsync(employee);
            return Ok();
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await employeeRepositorycs.GetAllEmployeesDetails();
            return Ok(employees);
        }

        [HttpDelete("delete/{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var result = await employeeRepositorycs.DeleteEmployeeAsync(employeeId);
            if (!result)
                return NotFound($"Employee with Id = {employeeId} not found.");

            return Ok("Employee deleted successfully.");
        }


    }
}
