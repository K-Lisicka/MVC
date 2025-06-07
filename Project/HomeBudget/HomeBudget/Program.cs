using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using HomeBudget.Data;
using HomeBudget.Areas.Identity.Data;
using HomeBudget.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HomeBudgetContext>(opt =>
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("HomeBudgetContext")
        ?? throw new InvalidOperationException("Connection string 'HomeBudgetContext' not found.")));

builder.Services.AddDbContext<HomeBudgetIdentityContext>(opt =>
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("HomeBudgetIdentityContextConnection")
        ?? throw new InvalidOperationException("Connection string 'HomeBudgetIdentityContextConnection' not found.")));

builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<HomeBudgetIdentityContext>(); 

builder.Services.Configure<SendGridOptions>(
    builder.Configuration.GetSection("SendGrid"));

Console.WriteLine("SG len = " +
    builder.Configuration["SendGrid:ApiKey"]?.Length);

builder.Services.AddTransient<IEmailSender, SendGridEmailSender>();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
