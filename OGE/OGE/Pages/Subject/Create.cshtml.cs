using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using OGE.Data;
using OGE.Hubs;
using SubjectModel = OGE.Model.Subject;
using SchoolchildrenModel = OGE.Model.Schoolchildren;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OGE.Pages.Subject
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

            await _hubContext.Clients.All.SendAsync("ReceiveUpdate", "Subject");

            return RedirectToPage("./Index");
        }
    }
}