using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OGE.Pages.Account
{
    [AllowAnonymous]
    public class AccessDeniedModel : PageModel { }
}