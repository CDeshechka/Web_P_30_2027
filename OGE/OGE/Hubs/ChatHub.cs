using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OGE.Data;
using OGE.Model;
using System;
using System.Threading.Tasks;

namespace OGE.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string message)
        {
            var userId = Context.UserIdentifier;
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return;

            var displayName = user.DisplayName ?? user.Email;
            var avatar = user.Avatar != null ? Convert.ToBase64String(user.Avatar) : "";
            var role = (await _context.UserRoles.AnyAsync(ur => ur.UserId == userId)) ? "Admin" : "";

            var chatMsg = new ChatMessage
            {
                UserId = userId,
                UserName = displayName,
                UserAvatar = avatar,
                Role = role,
                Text = message,
                Timestamp = DateTime.Now
            };

            _context.ChatMessages.Add(chatMsg);
            await _context.SaveChangesAsync();

            // Отправляем всем, включая отправителя, с Id сообщения
            await Clients.All.SendAsync("ReceiveMessage", chatMsg.Id, displayName, avatar, role, message, chatMsg.Timestamp.ToString("HH:mm"));
        }

        [Authorize(Roles = "Admin")]
        public async Task DeleteMessage(int messageId)
        {
            var msg = await _context.ChatMessages.FindAsync(messageId);
            if (msg != null)
            {
                _context.ChatMessages.Remove(msg);
                await _context.SaveChangesAsync();
                await Clients.All.SendAsync("ReceiveDelete", messageId);
            }
        }
    }
}