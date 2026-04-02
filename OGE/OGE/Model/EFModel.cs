using System.ComponentModel.DataAnnotations;
namespace OGE.Model
{
    public class EFModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [Display(Name = "Название")]
        public string Name { get; set; }
    }
}

