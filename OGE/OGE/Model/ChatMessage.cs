using System;
using System.ComponentModel.DataAnnotations;

namespace OGE.Model
{
    public class ChatMessage
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public string UserName { get; set; }       
        public string UserAvatar { get; set; }      
        public string Role { get; set; }            

        [Required]
        public string Text { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}