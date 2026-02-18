using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OGE.Model
{
    public class Schoolchildren : EFModel
    {
        public string Firstname {  get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public DateTime Dateofbirthday { get; set; }
    }
}
