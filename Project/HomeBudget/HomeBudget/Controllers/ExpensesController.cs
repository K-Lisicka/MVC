using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeBudget.Data;
using HomeBudget.Models;
using Microsoft.AspNetCore.Authorization;

namespace HomeBudget.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly HomeBudgetContext _context;

        public ExpensesController(HomeBudgetContext context)
        {
            _context = context;
        }

        // GET: Expenses
        [AllowAnonymous]
        public async Task<IActionResult> Index(string? sortOrder,
                                               DateTime? from,
                                               DateTime? to,
                                               string? category)
        {
            ViewBag.Categories = await _context.Expense
                                               .OrderBy(e => e.Category)
                                               .Select(e => e.Category)
                                               .Distinct()
                                               .ToListAsync();
            
            ViewData["CategorySort"] = sortOrder == "category" ? "category_desc" : "category";
            ViewData["AmountSort"] = sortOrder == "amount" ? "amount_desc" : "amount";
            ViewData["DateSort"] = sortOrder == "date" ? "date_desc" : "date";
            ViewData["SortOrder"] = sortOrder;

            var q = _context.Expense.AsQueryable();

            if (from.HasValue)
                q = q.Where(e => e.Date >= from.Value.Date);

            if (to.HasValue)
                q = q.Where(e => e.Date <= to.Value.Date);

            if (!string.IsNullOrWhiteSpace(category))
                q = q.Where(e => e.Category == category);

            q = sortOrder switch
            {
                "category" => q.OrderBy(e => e.Category),
                "category_desc" => q.OrderByDescending(e => e.Category),

                "amount" => q.OrderBy(e => e.Amount),
                "amount_desc" => q.OrderByDescending(e => e.Amount),

                "date" => q.OrderBy(e => e.Date),          
                "date_desc" => q.OrderByDescending(e => e.Date),

                _ => q.OrderBy(e => e.Date)           
            };


            var list = await q.ToListAsync();     

            ViewBag.Total = list.Sum(e => e.Amount);                            

            ViewData["From"] = from?.ToString("yyyy-MM-dd");
            ViewData["To"] = to?.ToString("yyyy-MM-dd");
            ViewData["Cat"] = category;

            return View(list);                                                   
        }

        // GET: Expenses/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category,Amount,Date")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,Amount,Date")] Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expense.FindAsync(id);
            if (expense != null)
            {
                _context.Expense.Remove(expense);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expense.Any(e => e.Id == id);
        }
    }
}
