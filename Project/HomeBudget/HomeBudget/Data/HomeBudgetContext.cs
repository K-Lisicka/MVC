using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HomeBudget.Models;

namespace HomeBudget.Data
{
    public class HomeBudgetContext : DbContext
    {
        public HomeBudgetContext (DbContextOptions<HomeBudgetContext> options)
            : base(options)
        {
        }

        public DbSet<HomeBudget.Models.Expense> Expense { get; set; } = default!;
    }
}
