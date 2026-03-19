using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OGE.Data;
using OGE.Model;
using System.Threading.Tasks;

namespace OGE.Pages.Schoolchildren
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OGE.Model.Schoolchildren Schoolchild { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Schoolchild = await _context.Schoolchildren.FirstOrDefaultAsync(s => s.Id == id);

            if (Schoolchild == null)
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

            Schoolchild = await _context.Schoolchildren.FindAsync(id);

            if (Schoolchild != null)
            {
                _context.Schoolchildren.Remove(Schoolchild);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
