using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBudget.Models;

public class Expense
{
    public int Id { get; set; }                 // primary key

    [Required, StringLength(50)]
    public string? Category { get; set; } = string.Empty;

    [Range(0.01, 1_000_000)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }

    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
}

