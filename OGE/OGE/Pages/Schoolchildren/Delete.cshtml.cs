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
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();
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