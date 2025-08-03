namespace SimpleCrudWebApp.Read
{
    public class SalaryDetails
    {
        public int SalaryDetailId { get; set; }
        public int EmployeeId { get; set; }

        public int? PayrollItem { get; set; }
        public string? ItemType { get; set; }
        public string? Name { get; set; }
        public decimal? Value { get; set; }

       
    }
}
