using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OGE.Data;
using OGE.Hubs;
using SchoolchildrenModel = OGE.Model.Schoolchildren;
using System.Threading.Tasks;

namespace OGE.Pages.Schoolchildren
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<UpdateHub> _hubContext;

        public EditModel(ApplicationDbContext context, IHubContext<UpdateHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
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
                await _hubContext.Clients.All.SendAsync("ReceiveUpdate", "Schoolchildren");
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