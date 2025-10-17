using Microsoft.EntityFrameworkCore;
using SimpleCrudWebApp.Interface;
using SimpleCrudWebApp.Models;


namespace SimpleCrudWebApp.Implementation
{

    public class EmployeeRepository : IEmployeeRepositorycs
    {
        private readonly OrgDbContext orgDbContext;
        private readonly ISalaryDetailsRepository salaryDetailsRepository;

        public EmployeeRepository(OrgDbContext orgDbContext, ISalaryDetailsRepository salaryDetailsRepository)
        {
            this.orgDbContext = orgDbContext;
            this.salaryDetailsRepository = salaryDetailsRepository;
        }

        public async Task CreateAsync(SimpleCrudWebApp.Write.Employee.Create createEmployee)
        {

            var newEmployee = new SimpleCrudWebApp.Models.Employee
            {

                FirstName = createEmployee.FirstName,
                LastName = createEmployee.LastName,
                EmailId = createEmployee.Email,
                Location = createEmployee.Location,
                DateOfBirth = createEmployee.DateOfBirth
            };

            await orgDbContext.Employees.AddAsync(newEmployee);
            await orgDbContext.SaveChangesAsync(); 

        }


        public async Task UpdateAsync(SimpleCrudWebApp.Write.Employee.Update employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            var existingEmployee = await orgDbContext.Employees
                .Include(e => e.SalaryDetails)
                .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

            if (existingEmployee == null) throw new Exception("Employee not found");

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.EmailId = employee.Email;
            existingEmployee.Location = employee.Location;
            existingEmployee.DateOfBirth = employee.DateOfBirth;

            await orgDbContext.SaveChangesAsync();
        }


        public async Task<List<SimpleCrudWebApp.Read.Employee>> GetAllEmployeesDetails()
        {
            var employees = await orgDbContext.Employees
                .Include(e => e.SalaryDetails)
                .ToListAsync();

            var result = employees.Select(e => new SimpleCrudWebApp.Read.Employee
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.EmailId,
                Location = e.Location,
                DateOfBirth = e.DateOfBirth,
            }).ToList();

            return result;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await orgDbContext.Employees
                .Include(e => e.SalaryDetails)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
                return false;

            orgDbContext.Employees.Remove(employee);

            await orgDbContext.SaveChangesAsync();
            return true;
        }


    }
}
