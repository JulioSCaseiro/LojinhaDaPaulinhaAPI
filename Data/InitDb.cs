using LojinhaDaPaulinhaAPI.Entities;
using LojinhaDaPaulinhaAPI.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LojinhaDaPaulinhaAPI.Data
{
    public class InitDb
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;
        private readonly IIdentityManager _identityManager;
        private readonly IDataUnit _dataUnit;

        public InitDb(
            IConfiguration configuration,
            DataContext dataContext,
            IIdentityManager identityManager,
            IDataUnit dataUnit)
        {
            _configuration = configuration;
            _dataContext = dataContext;
            _identityManager = identityManager;
            _dataUnit = dataUnit;
        }

        public async Task SeedAsync()
        {
            await MigrateAsync();

            await SeedRolesAsync();
            await SeedUsersAsync();
            
            //await SeedProductsAsync();

            await SaveChangesAsync();
        }

        private async Task MigrateAsync()
        {
            await _dataContext.Database.MigrateAsync();
        }

        public async Task SeedRolesAsync()
        {
            var defaultRoles = _configuration["SeedDb:Roles"].Split(',');

            foreach (var roleName in defaultRoles)
            {
                if (!await _identityManager.RoleExistsAsync(roleName))
                {
                    await _identityManager.CreateRoleAsync(new IdentityRole(roleName));
                }
            }
        }

        public async Task SeedUsersAsync()
        {
            // -- Create users and assign role --

            var defaultUsers = _configuration["SeedDb:Users:DefaultUsers"].Split(',');

            foreach (var defaultUser in defaultUsers)
            {
                if (!await _identityManager.UserExistsAsync(defaultUser))
                {
                    // Get necessary data from configuration file.
                    var userName = _configuration[$"SeedDb:Users:{defaultUser}:UserName"];
                    var password = _configuration[$"SeedDb:Users:{defaultUser}:Password"];
                    var roleName = _configuration[$"SeedDb:Users:{defaultUser}:Role"];

                    // New user.
                    var user = new AppUser { UserName = userName, Role = roleName };

                    // Save user in database.
                    var createUser = await _identityManager.CreateUserAsync(user, password);
                    if (createUser == null || !createUser.Succeeded)
                    {
                        throw new Exception($"Could not create user {userName}. {createUser?.Errors}");
                    }

                    // Add user to role.
                    var setRoleOfUser = await _identityManager.SetRoleOfUserAsync(user, roleName);
                    if (setRoleOfUser == null || !setRoleOfUser.Succeeded)
                    {
                        throw new Exception($"Could not set user's role: user {userName}, role {roleName}. {setRoleOfUser?.Errors}");
                    }
                }
            }
        }

        //public async Task SeedProductsAsync()
        //{
        //    var defaultProducts = _configuration["SeedDb:Products"].Split(',');

        //    foreach (var productName in defaultProducts)
        //    {
        //        if (!await _dataUnit.Products.ExistsAsync(productName))
        //        {
        //            await _dataUnit.Products.CreateAsync(new Product { Name = productName });
        //        }
        //    }
        //}

        private async Task SaveChangesAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
