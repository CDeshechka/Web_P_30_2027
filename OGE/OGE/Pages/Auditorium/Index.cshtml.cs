using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OGE.Data;
using AuditoriumModel = OGE.Model.Auditorium;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OGE.Pages.Auditorium
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<AuditoriumModel> Auditoriums { get; set; }

        public async Task OnGetAsync()
        {
            Auditoriums = await _context.Auditorium.ToListAsync();
        }
    }
}