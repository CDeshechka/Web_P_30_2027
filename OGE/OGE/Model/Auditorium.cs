using System.ComponentModel.DataAnnotations;
namespace OGE.Model
{
    public class Auditorium : EFModel
    {
        [Required(ErrorMessage = "Номер аудитории обязателен")]
        [Range(1, 1000, ErrorMessage = "Номер аудитории должен быть положительным числом")]
        [Display(Name = "Номер аудитории")]
        public double Auditoriumnumber { get; set; }

        [Required(ErrorMessage = "Вместимость обязательна")]
        [Range(1, 500, ErrorMessage = "Вместимость должна быть от 1 до 500")]
        [Display(Name = "Вместимость")]
        public double Auditoriumcapacity { get; set; }

        [Required(ErrorMessage = "Название предмета обязательно")]
        [Display(Name = "Предмет")]
        public string Auditoriumsubject { get; set; }  
    }
}