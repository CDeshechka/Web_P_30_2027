using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OGE.Data;
using SchoolchildrenModel = OGE.Model.Schoolchildren;   // псевдоним
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

        public IList<SchoolchildrenModel> SchoolchildrenList { get; set; }

        public async Task OnGetAsync()
        {
            SchoolchildrenList = await _context.Schoolchildren.ToListAsync();
        }
    }
}