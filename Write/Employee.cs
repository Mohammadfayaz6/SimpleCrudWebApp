
namespace SimpleCrudWebApp.Write.Employee
{
    public class Create
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }
        public DateOnly? DateOfBirth  { get; set; }

        public List<SalaryDetailDto> SalaryDetails { get; set; }


    }

    public class Update
    {
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public List<SalaryDetailDto> SalaryDetails { get; set; }


    }

    public class SalaryDetailDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int? PayrollItem { get; set; }
        public string? ItemType { get; set; }
        public string? Name { get; set; }
        public decimal? Value { get; set; }
    }
}
