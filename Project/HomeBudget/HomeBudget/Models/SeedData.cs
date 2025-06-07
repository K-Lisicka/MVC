using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HomeBudget.Data;
using System;
using System.Linq;

namespace HomeBudget.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new HomeBudgetContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<HomeBudgetContext>>()))
        {
            // Look for any expenses
            if (context.Expense.Any())
            {
                return;   // DB has been seeded
            }
            context.Expense.AddRange(
                new Expense
                {
                    Category = "Food & Beverage",
                    Amount = 13.20M,
                    Date = DateTime.Parse("2025-5-27"),
                },
                new Expense
                {
                    Category = "Cosmetics & Beauty",
                    Amount = 254.70M,
                    Date = DateTime.Parse("2025-5-23"),
                },
                new Expense
                {
                    Category = "Housing & Bills",
                    Amount = 3534.63M,
                    Date = DateTime.Parse("2025-5-12"),
                }
            );
            context.SaveChanges();
        }
    }
}
