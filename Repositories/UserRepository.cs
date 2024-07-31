using LojinhaDaPaulinhaAPI.Dtos;
using LojinhaDaPaulinhaAPI.Identity;
using LojinhaDaPaulinhaAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LojinhaDaPaulinhaAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IIdentityManager _identityManager;

        public UserRepository(IIdentityManager userManager)
        {
            _identityManager = userManager;
        }


        public async Task<IdentityResult> CreateAsync(AppUser user, string password)
        {
            return await _identityManager.CreateUserAsync(user, password);
        }

        public async Task<IdentityResult> CreateInRoleAsync(AppUser user, string password, string roleName)
        {
            var createUser = await _identityManager.CreateUserAsync(user, password);
            if (createUser.Succeeded)
            {
                return await _identityManager.SetRoleOfUserAsync(user, roleName);
            }
            else
            {
                return createUser;
            }
        }

        public async Task<IdentityResult> DeleteAsync(string id)
        {
            return await _identityManager.DeleteUserAsync(id);
        }

        public IQueryable<AppUser> GetAllAsNoTracking()
        {
            return _identityManager.Users.AsNoTracking();
        }

        public async Task<IEnumerable<UserDto>> GetAllOrderByPropertiesAsync(params string[] propertiesNames)
        {
            return await _identityManager.Users
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Role = user.Role
                })
                .ToListAsync();
        }

        public async Task<AppUser> FindByIdAsync(string id)
        {
            return await _identityManager.FindByIdAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetComboRolesAsync()
        {
            var roles = await _identityManager.Roles.Select(role => new SelectListItem
            {
                Text = role.Name,
                Value = role.Name,
            }).ToListAsync();

            var roleOrder = new List<string> { "Admin", "Technician", "Client" };

            return roles.OrderBy(role => roleOrder.IndexOf(role.Text));
        }

        public async Task<UserDto> GetUserViewModelAsync(string id)
        {
            return await _identityManager.Users
                .Where(user => user.Id == id)
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Role = user.Role
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IdentityResult> SetNewPasswordAsync(AppUser user, string newPassword)
        {
            return await _identityManager.SetPasswordAsync(user, newPassword);
        }

        public async Task<IdentityResult> SetRoleAsync(AppUser user, string roleName)
        {
            return await _identityManager.SetRoleOfUserAsync(user, roleName);
        }

        public async Task<IdentityResult> UpdateAsync(AppUser user)
        {
            return await _identityManager.UpdateUserAsync(user);
        }
    }
}
