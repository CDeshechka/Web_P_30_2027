using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using OGE.Data;
using OGE.Hubs;
using AuditoriumModel = OGE.Model.Auditorium;
using System.Threading.Tasks;

namespace OGE.Pages.Auditorium
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<UpdateHub> _hubContext;

        public CreateModel(ApplicationDbContext context, IHubContext<UpdateHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
        public AuditoriumModel Auditorium { get; set; }

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Auditorium.Add(Auditorium);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("ReceiveUpdate", "Auditorium");

            return RedirectToPage("./Index");
        }
    }
}