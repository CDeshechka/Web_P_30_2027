using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OGE.Data;
using SubjectModel = OGE.Model.Subject;   
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OGE.Pages.Subject
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SubjectModel> Subjects { get; set; }

        public async Task OnGetAsync()
        {
            Subjects = await _context.Subject
                .Include(s => s.Schoolchildren)
                .ToListAsync();
        }
    }
}