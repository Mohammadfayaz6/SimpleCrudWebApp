namespace SimpleCrudWebApp.Write.SalaryDetails
{
    public class Create
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int? PayRollItem { get; set; }
        public string? ItemType { get; set; }
        public string? Name { get; set; }
        public decimal? Value { get; set; }


    }
    public class Update
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int? PayRollItem { get; set; }
        public string? ItemType { get; set; }
        public string? Name { get; set; }
        public decimal? Value { get; set; }
    }
}
