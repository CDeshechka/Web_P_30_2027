using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OGE.Data;
using OGE.Model;
using System.Threading.Tasks;

namespace OGE.Pages.Auditorium
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OGE.Model.Auditorium Auditorium { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Auditorium.Add(Auditorium);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
