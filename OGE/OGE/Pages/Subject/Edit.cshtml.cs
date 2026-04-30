using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OGE.Data;
using SubjectModel = OGE.Model.Subject;
using SchoolchildrenModel = OGE.Model.Schoolchildren;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OGE.Pages.Subject
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SubjectModel Subject { get; set; }

        public List<SchoolchildrenModel> SchoolchildrenList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Subject = await _context.Subject.FirstOrDefaultAsync(s => s.Id == id);

            if (Subject == null)
            {
                return NotFound();
            }

            SchoolchildrenList = _context.Schoolchildren.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                SchoolchildrenList = _context.Schoolchildren.ToList();
                return Page();
            }

            _context.Attach(Subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(Subject.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SubjectExists(int id)
        {
            return _context.Subject.Any(e => e.Id == id);
        }
    }
}