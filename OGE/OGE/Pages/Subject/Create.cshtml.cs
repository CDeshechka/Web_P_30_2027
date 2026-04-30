using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OGE.Data;
using SubjectModel = OGE.Model.Subject;
using SchoolchildrenModel = OGE.Model.Schoolchildren;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OGE.Pages.Subject
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SubjectModel Subject { get; set; }

        public List<SchoolchildrenModel> SchoolchildrenList { get; set; }

        public void OnGet()
        {
            SchoolchildrenList = _context.Schoolchildren.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                SchoolchildrenList = _context.Schoolchildren.ToList();
                return Page();
            }

            _context.Subject.Add(Subject);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}