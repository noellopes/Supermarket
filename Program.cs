using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GroupsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GroupsConnection") ?? throw new InvalidOperationException("Connection string 'GroupsConnection' not found.")));

builder.Services.AddDbContext<SupermarketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SupermarketConnection") ?? throw new InvalidOperationException("Connection string 'SupermarketDbContext' not found.")));

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    using var serviceScope = app.Services.CreateScope();
    var db = serviceScope.ServiceProvider.GetService<SupermarketDbContext>();
    SeedData.Populate(db!);
}else{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

app.Run();
