using Microsoft.AspNetCore.Identity;

namespace LojinhaDaPaulinhaAPI.Identity
{
    public class AppUser : IdentityUser
    {
        private string _userName;

        public override string UserName
        {
            get => _userName;
            set => _userName = value.Trim();
        }

        public string Role { get; set; }
    }
}
