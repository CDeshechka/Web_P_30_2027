using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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
        public SubjectModel Subject { get; set; }

        public List<SchoolchildrenModel> SchoolchildrenList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();
            Subject = await _context.Subject.FirstOrDefaultAsync(s => s.Id == id);
            if (Subject == null) return NotFound();

            SchoolchildrenList = _context.Schoolchildren.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                SchoolchildrenList = _context.Schoolchildren.ToList();
                return Page();
            }

            _context.Attach(Subject).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("ReceiveUpdate", "Subject");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Subject.Any(e => e.Id == Subject.Id))
                    return NotFound();
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}