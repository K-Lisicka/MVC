using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeBudget.Models;  

public class ExpenseCategoryViewModel
{
    public List<Expense>? Expenses { get; set; }
    public SelectList? Categories { get; set; }
    public string? ExpenseCategory { get; set; }
    public string? SearchString { get; set; }
}
