using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SimpleCrudWebApp.Models;

public partial class SalaryDetail
{
    [Key]
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int? PayrollItem { get; set; }

    [StringLength(50)]
    public string? ItemType { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Value { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("SalaryDetails")]
    public virtual Employee Employee { get; set; } = null!;
}
