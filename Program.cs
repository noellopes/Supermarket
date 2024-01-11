using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;
using Supermarket.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<GroupsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GroupsConnection") ?? throw new InvalidOperationException("Connection string 'GroupsConnection' not found.")));

builder.Services.AddDbContext<SupermarketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SupermarketConnection") ?? throw new InvalidOperationException("Connection string 'SupermarketDbContext' not found.")));

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

/*builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<SupermarketDbContext>()
    .AddDefaultUI();
*/

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    options => {
        // Sign in
        options.SignIn.RequireConfirmedAccount = false;

        // Password
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredLength = 8;
        options.Password.RequiredUniqueChars = 6;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;

        // Lockout
        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        options.Lockout.MaxFailedAccessAttempts = 5;
    })

    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();

builder.Services.AddControllersWithViews();

var app = builder.Build();

var reqServScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
var roleManager = reqServScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//SeedData.PopulateRolesAsync(roleManager).Wait();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    using var serviceScope = app.Services.CreateScope();
    var db = serviceScope.ServiceProvider.GetService<SupermarketDbContext>();
    //SeedData.Populate(db!);

    var userManager = reqServScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    //SeedData.PopulateDevUsers(userManager);

}
else
{

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

// Chama fun��o para atualizar o status de expira��o
UpdateExpirationStatusForAllPurchases();

void UpdateExpirationStatusForAllPurchases()
{
    // Cria um novo escopo para inje��o de depend�ncia
    using var serviceScope = app.Services.CreateScope();

    // Obt�m uma inst�ncia de SupermarketDbContext do provedor de servi�os no escopo criado
    var purchasesController = new PurchasesController(serviceScope.ServiceProvider.GetRequiredService<SupermarketDbContext>());

    // Chama o m�todo UpdateExpirationStatusForAllPurchases() no controller
    purchasesController.UpdateExpirationStatusForAllPurchases();
}

app.Run();