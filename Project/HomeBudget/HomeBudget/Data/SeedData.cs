using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HomeBudget.Models;
using System;
using System.Linq;

namespace HomeBudget.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new HomeBudgetContext(
                serviceProvider.GetRequiredService<DbContextOptions<HomeBudgetContext>>());

            if (context.Expense.Any()) return;

            context.Expense.AddRange(
                new Expense { Category = "Food & Beverage", Amount = 23.50m, Date = new DateTime(2025, 6, 1) },
                new Expense { Category = "Housing & Bills", Amount = 1200.00m, Date = new DateTime(2025, 6, 3) },
                new Expense { Category = "Entertainment", Amount = 75.00m, Date = new DateTime(2025, 6, 5) }
            );
            context.SaveChanges();
        }
    }
}

