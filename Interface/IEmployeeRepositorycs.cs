using SimpleCrudWebApp.Models;

namespace SimpleCrudWebApp.Interface
{
    public interface IEmployeeRepositorycs
    {
        
        Task CreateAsync(SimpleCrudWebApp.Write.Employee.Create employee);
        Task UpdateAsync(SimpleCrudWebApp.Write.Employee.Update employee);
        Task<List<SimpleCrudWebApp.Read.Employee>> GetAllEmployeesDetails();
        Task<bool> DeleteEmployeeAsync(int employeeId);


    }
}
