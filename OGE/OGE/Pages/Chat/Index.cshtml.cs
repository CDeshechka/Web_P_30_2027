using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OGE.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OGE.Pages.Chat
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public string MessagesJson { get; set; } = "[]";
        public bool IsAdmin { get; set; }

        public async Task OnGetAsync()
        {
            IsAdmin = User.IsInRole("Admin");

            var messages = await _context.ChatMessages
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            var result = messages.Select(m => new
            {
                id = m.Id,
                userName = m.UserName,
                avatarBase64 = m.UserAvatar ?? "",
                role = m.Role ?? "",
                text = m.Text,
                time = m.Timestamp.ToString("HH:mm")
            }).ToList();

            MessagesJson = JsonSerializer.Serialize(result);
        }
    }
}