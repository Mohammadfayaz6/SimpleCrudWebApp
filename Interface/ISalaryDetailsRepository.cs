namespace SimpleCrudWebApp.Interface
{

    public interface ISalaryDetailsRepository
    {
        Task CreateAsync(SimpleCrudWebApp.Write.SalaryDetails.Create createSalaryDetails);
        Task UpdateAsync(SimpleCrudWebApp.Write.SalaryDetails.Update createSalaryDetails);
        
    }
}
