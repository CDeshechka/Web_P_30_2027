using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OGE.Data;
using AuditoriumModel = OGE.Model.Auditorium;
using System.Threading.Tasks;

namespace OGE.Pages.Auditorium
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AuditoriumModel Auditorium { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Auditorium = await _context.Auditorium.FirstOrDefaultAsync(a => a.Id == id);
            if (Auditorium == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Attach(Auditorium).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Auditorium.Any(e => e.Id == Auditorium.Id))
                    return NotFound();
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}