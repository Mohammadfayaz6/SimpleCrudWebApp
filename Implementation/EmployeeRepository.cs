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
            if (createEmployee == null)
                throw new ArgumentNullException(nameof(createEmployee), "Employee cannot be null");

            var newEmployee = new SimpleCrudWebApp.Models.Employee
            {

                FirstName = createEmployee.FirstName,
                LastName = createEmployee.LastName,
                EmailId = createEmployee.Email,
                Location = createEmployee.Location,
                DateOfBirth = createEmployee.DateOfBirth
            };

            await orgDbContext.Employees.AddAsync(newEmployee);
            await orgDbContext.SaveChangesAsync(); // Save to get EmployeeId

            // Insert SalaryDetails using the salary repository
            if (createEmployee.SalaryDetails != null && createEmployee.SalaryDetails.Any())
            {
                foreach (var salary in createEmployee.SalaryDetails)
                {
                    var salaryDto = new SimpleCrudWebApp.Write.SalaryDetails.Create
                    {
                        EmployeeId = newEmployee.EmployeeId, // Use the saved employee's ID
                        PayRollItem = salary.PayrollItem,
                        ItemType = salary.ItemType,
                        Name = salary.Name,
                        Value = salary.Value
                    };

                    await salaryDetailsRepository.CreateAsync(salaryDto);
                }
            }

            // Save salary records
            await orgDbContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(SimpleCrudWebApp.Write.Employee.Update employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            var existingEmployee = await orgDbContext.Employees
                .Include(e => e.SalaryDetails)
                .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

            if (existingEmployee == null) throw new Exception("Employee not found");

            // Update employee fields
            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.EmailId = employee.Email;
            existingEmployee.Location = employee.Location;
            existingEmployee.DateOfBirth = employee.DateOfBirth;

            // If salary details are provided, update/add
            if (employee.SalaryDetails != null && employee.SalaryDetails.Any())
            {
                foreach (var salaryDto in employee.SalaryDetails)
                {
                    if (salaryDto.Id != null && salaryDto.Id != 0)
                    {
                        // Update existing salary detail
                        var existingSalary = existingEmployee.SalaryDetails
                            .FirstOrDefault(s => s.Id == salaryDto.Id);

                        if (existingSalary != null)
                        {
                            existingSalary.PayrollItem = salaryDto.PayrollItem;
                            existingSalary.ItemType = salaryDto.ItemType;
                            existingSalary.Name = salaryDto.Name;
                            existingSalary.Value = salaryDto.Value;
                        }
                    }
                    else
                    {
                        // Add new salary detail
                        existingEmployee.SalaryDetails.Add(new SalaryDetail
                        {
                            PayrollItem = salaryDto.PayrollItem,
                            ItemType = salaryDto.ItemType,
                            Name = salaryDto.Name,
                            Value = salaryDto.Value,
                            EmployeeId = employee.EmployeeId // important if not using navigation
                        });
                    }
                }
            }

            await orgDbContext.SaveChangesAsync();
        }


        public async Task<List<SimpleCrudWebApp.Read.Employee>> GetAllEmployeesDetails()
        {
            var employees = await orgDbContext.Employees
                .Include(e => e.SalaryDetails) // Eager load salary details
                .ToListAsync();

            var result = employees.Select(e => new SimpleCrudWebApp.Read.Employee
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.EmailId,
                Location = e.Location,
                DateOfBirth = e.DateOfBirth,
                SalaryDetails = e.SalaryDetails?.Select(s => new SimpleCrudWebApp.Read.SalaryDetail
                {
                    Id = s.Id,
                    PayrollItem = s.PayrollItem ?? 0, // Handle possible null values
                    ItemType = s.ItemType ?? string.Empty, // Handle possible null values
                    Name = s.Name ?? string.Empty, // Handle possible null values
                    Value = s.Value ?? 0 // Handle possible null values
                }).ToList() ?? new List<SimpleCrudWebApp.Read.SalaryDetail>() // Handle null collection
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

            // First remove related salary details
            orgDbContext.RemoveRange(employee.SalaryDetails);

            // Then remove the employee
            orgDbContext.Employees.Remove(employee);

            await orgDbContext.SaveChangesAsync();
            return true;
        }


    }
}
