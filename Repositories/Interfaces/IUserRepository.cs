using LojinhaDaPaulinhaAPI.Dtos;
using LojinhaDaPaulinhaAPI.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojinhaDaPaulinhaAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateAsync(AppUser user, string password);
        Task<IdentityResult> CreateInRoleAsync(AppUser user, string password, string roleName);
        Task<IdentityResult> DeleteAsync(string id);
        Task<AppUser> FindByIdAsync(string id);
        IQueryable<AppUser> GetAllAsNoTracking();
        Task<IEnumerable<UserDto>> GetAllOrderByPropertiesAsync(params string[] propertiesNames);
        Task<IEnumerable<SelectListItem>> GetComboRolesAsync();
        Task<UserDto> GetUserViewModelAsync(string id);
        Task<IdentityResult> SetNewPasswordAsync(AppUser user, string newPassword);
        Task<IdentityResult> SetRoleAsync(AppUser user, string roleName);
        Task<IdentityResult> UpdateAsync(AppUser user);
    }
}
