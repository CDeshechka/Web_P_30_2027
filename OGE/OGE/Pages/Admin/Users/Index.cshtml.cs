using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OGE.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OGE.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IList<UserViewModel> Users { get; set; }

        public async Task OnGetAsync()
        {
            var users = _userManager.Users.ToList();
            Users = new List<UserViewModel>();

            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                Users.Add(new UserViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    DisplayName = u.DisplayName,
                    Roles = roles.ToList()
                });
            }
        }

        public class UserViewModel
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string? DisplayName { get; set; }
            public List<string> Roles { get; set; }
        }
    }
}