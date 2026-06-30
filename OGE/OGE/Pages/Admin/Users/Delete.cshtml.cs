using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OGE.Model;
using System.Threading.Tasks;

namespace OGE.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public string UserId { get; set; }
        public string UserEmail { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            UserId = user.Id;
            UserEmail = user.Email;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UserId == null) return NotFound();

            var user = await _userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Не удалось удалить пользователя.");
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}