// Program.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using Supermarket.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddDbContext<GroupsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GroupsConnection") ?? throw new InvalidOperationException("Connection string 'GroupsConnection' not found.")));

builder.Services.AddDbContext<SupermarketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SupermarketConnection") ?? throw new InvalidOperationException("Connection string 'SupermarketDbContext' not found.")));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    options => {
        // Configurações de SignIn, Password, Lockout...
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();

builder.Services.AddControllersWithViews();

var app = builder.Build();

var reqServScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
var roleManager = reqServScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

 SeedData.PopulateRolesAsync(roleManager).Wait();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    using var serviceScope = app.Services.CreateScope();
    var db = serviceScope.ServiceProvider.GetService<SupermarketDbContext>();
    SeedData.Populate(db!);

    var userManager = reqServScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    SeedData.PopulateDevUsers(userManager);
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

UpdateExpirationStatusForAllPurchases();

void UpdateExpirationStatusForAllPurchases()
{
    using var serviceScope = app.Services.CreateScope();
    var purchasesController = new PurchasesController(serviceScope.ServiceProvider.GetRequiredService<SupermarketDbContext>());
    // purchasesController.UpdateExpirationStatusForAllPurchases();
}

app.Run();
