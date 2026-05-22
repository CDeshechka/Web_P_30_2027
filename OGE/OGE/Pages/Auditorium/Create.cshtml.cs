using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OGE.Data;
using AuditoriumModel = OGE.Model.Auditorium;
using System.Threading.Tasks;

namespace OGE.Pages.Auditorium
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AuditoriumModel Auditorium { get; set; }

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Auditorium.Add(Auditorium);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}