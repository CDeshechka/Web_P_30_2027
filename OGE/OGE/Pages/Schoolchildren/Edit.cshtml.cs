using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OGE.Data;
using SchoolchildrenModel = OGE.Model.Schoolchildren;
using System.Threading.Tasks;

namespace OGE.Pages.Schoolchildren
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
        public SchoolchildrenModel Schoolchild { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();
            Schoolchild = await _context.Schoolchildren.FirstOrDefaultAsync(s => s.Id == id);
            if (Schoolchild == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Attach(Schoolchild).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Schoolchildren.Any(e => e.Id == Schoolchild.Id))
                    return NotFound();
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}