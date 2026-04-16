using System.ComponentModel.DataAnnotations;
namespace OGE.Model
{
    public class EFModel
    {
        public int Id { get; set; }

       [Display(Name = "Имя")]
        public string Name { get; set; }
    }
}

