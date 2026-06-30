using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OGE.Data;
using OGE.Hubs;
using AuditoriumModel = OGE.Model.Auditorium;
using System.Threading.Tasks;

namespace OGE.Pages.Auditorium
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
        public AuditoriumModel Auditorium { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();
            Auditorium = await _context.Auditorium.FirstOrDefaultAsync(a => a.Id == id);
            if (Auditorium == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Attach(Auditorium).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("ReceiveUpdate", "Auditorium");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Auditorium.Any(e => e.Id == Auditorium.Id))
                    return NotFound();
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}