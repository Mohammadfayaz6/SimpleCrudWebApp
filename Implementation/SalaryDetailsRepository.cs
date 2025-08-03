using SimpleCrudWebApp.Interface;
using SimpleCrudWebApp.Models;

namespace SimpleCrudWebApp.Implementation
{
    public class SalaryDetailsRepository : ISalaryDetailsRepository
    {
        private readonly OrgDbContext orgDbContext;

        public SalaryDetailsRepository(OrgDbContext orgDbContext)
        {
            this.orgDbContext = orgDbContext;
        }

        public async Task CreateAsync(SimpleCrudWebApp.Write.SalaryDetails.Create salaryDetail)
        {
            if (salaryDetail == null)
            {
                throw new ArgumentNullException(nameof(salaryDetail), "Salary detail cannot be null");
            }
            var newSalaryDetail = new SimpleCrudWebApp.Models.SalaryDetail
            {
                EmployeeId = salaryDetail.EmployeeId,
                Value = salaryDetail.Value,
                PayrollItem = salaryDetail.PayRollItem,
                ItemType = salaryDetail.ItemType,
                Name = salaryDetail.Name

            };
            await orgDbContext.SalaryDetails.AddAsync(newSalaryDetail);
        }

        public async Task UpdateAsync(SimpleCrudWebApp.Write.SalaryDetails.Update salaryDetail)
        {
            if (salaryDetail == null)
            {
                throw new ArgumentNullException(nameof(salaryDetail), "Salary detail cannot be null");
            }
            var existingSalaryDetail = orgDbContext.SalaryDetails.FirstOrDefault(e => e.Id == salaryDetail.Id);
            if (existingSalaryDetail != null)
            {
                existingSalaryDetail.EmployeeId = salaryDetail.EmployeeId;
                existingSalaryDetail.Value = salaryDetail.Value;
                existingSalaryDetail.PayrollItem = salaryDetail.PayRollItem;
                existingSalaryDetail.ItemType = salaryDetail.ItemType;
                existingSalaryDetail.Name = salaryDetail.Name;
            }

             orgDbContext.Update(existingSalaryDetail);
            // Simulate async operation
        }

    }
}
