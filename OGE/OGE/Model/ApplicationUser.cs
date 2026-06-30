using Microsoft.AspNetCore.Identity;

namespace OGE.Model
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? DisplayName { get; set; }
        public byte[]? Avatar { get; set; }
    }
}