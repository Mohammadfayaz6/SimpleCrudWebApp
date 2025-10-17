namespace SimpleCrudWebApp.Read
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }
        public DateOnly? DateOfBirth { get; set; }

        //public List<SalaryDetail> SalaryDetails { get; set; }
    }

    // Read/SalaryDetail.cs
    public class SalaryDetail
    {

        public int Id { get; set; }
        public int? PayrollItem { get; set; }
        public string? ItemType { get; set; }
        public string? Name { get; set; }
        public decimal? Value { get; set; }
    }

}
