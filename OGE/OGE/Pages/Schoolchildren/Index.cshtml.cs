using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OGE.Data;
using OGE.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OGE.Pages.Schoolchildren
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OGE.Model.Schoolchildren> SchoolchildrenList { get; set; }

        public async Task OnGetAsync()
        {
            SchoolchildrenList = await _context.Schoolchildren.ToListAsync();
        }
    }
}
