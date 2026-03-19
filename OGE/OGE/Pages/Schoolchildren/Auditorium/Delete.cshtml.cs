using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OGE.Data;
using OGE.Model;
using System.Threading.Tasks;

namespace OGE.Pages.Auditorium
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OGE.Model.Auditorium Auditorium { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Auditorium = await _context.Auditorium.FirstOrDefaultAsync(a => a.Id == id);

            if (Auditorium == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Auditorium = await _context.Auditorium.FindAsync(id);

            if (Auditorium != null)
            {
                _context.Auditorium.Remove(Auditorium);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}