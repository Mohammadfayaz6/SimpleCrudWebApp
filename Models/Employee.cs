using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SimpleCrudWebApp.Models;

public partial class Employee
{
    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [Column("EmailID")]
    [StringLength(100)]
    public string? EmailId { get; set; }

    [StringLength(100)]
    public string? Location { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    [Key]
    public int EmployeeId { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<SalaryDetail> SalaryDetails { get; set; } = new List<SalaryDetail>();
}
