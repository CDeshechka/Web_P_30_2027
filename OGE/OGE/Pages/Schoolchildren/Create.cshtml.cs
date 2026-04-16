using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OGE.Data;
using OGE.Model;
using System.Threading.Tasks;

namespace OGE.Pages.Schoolchildren
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OGE.Model.Schoolchildren Schoolchild { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
        
            Schoolchild.Name = $"{Schoolchild.Lastname} {Schoolchild.Firstname}";

           
            ModelState.Remove("Schoolchild.Name");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Schoolchildren.Add(Schoolchild);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}