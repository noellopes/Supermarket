using Supermarket.Models;
using Supermarket.Data;
using Microsoft.AspNetCore.Identity;

namespace Books.Data
{
    public class SeedData
    {

        private const string ROLE_ADMIN = "Administrator";
        private const string ROLE_CLIENT = "Client";
        private const string ROLE_ADMIN1 = "Funcionário";
        private const string ROLE_ADMIN2 = "Gestor";
        private const string ROLE_ADMIN3 = "Cliente";
        private const string ROLE_STOCK_ADMIN = "Stock Administrator";
        private const string ROLE_STOCK_OP = "Stock Operator";
        private const string ROLE_MANAGER = "Manager";
        private const string ROLE_EMPLOYEER = "Employeer";
        private const string ROLE_REGISTER = "Cash Register";

        internal static void Populate(SupermarketDbContext db)
        {
            PopulateReserve(db);
            
            
        }

        

        internal static async void PopulateDevUsers(UserManager<IdentityUser>? userManager)
        {
            var userluzAdmin = await EnsureUserIsCreatedAsync(userManager!, "luz@ipg.pt", "Secret#123");
            var usertestEmp = await EnsureUserIsCreatedAsync(userManager!, "test@ipg.pt", "Secret#123");

            if (!await userManager!.IsInRoleAsync(userluzAdmin, ROLE_ADMIN))
            {
                await userManager!.AddToRoleAsync(userluzAdmin, ROLE_ADMIN);
            }
            if (!await userManager!.IsInRoleAsync(usertestEmp, ROLE_EMPLOYEER))
            {
                await userManager!.AddToRoleAsync(usertestEmp, ROLE_EMPLOYEER);
            }
        }


        private static async Task<IdentityUser> EnsureUserIsCreatedAsync(UserManager<IdentityUser> userManager, string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user == null)
            {

                user = new IdentityUser(username);
                await userManager.CreateAsync(user, password);
            }

            return user;

        }






    }
}
