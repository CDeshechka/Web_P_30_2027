using System.ComponentModel.DataAnnotations;
using SchoolchildrenModel = OGE.Model.Schoolchildren;

namespace OGE.Model
{
    public class Subject : EFModel
    {
        // Название предмета обязательно
        [Required(ErrorMessage = "Название предмета обязательно")]
        [Display(Name = "Название")]
        public new string? Name { get; set; }

        [Required(ErrorMessage = "Оценка за ОГЭ обязательна")]
        [Range(2, 5, ErrorMessage = "Оценка должна быть от 2 до 5")]
        [Display(Name = "Оценка за ОГЭ")]
        public double Assessmentfortheoge { get; set; }

        [Required(ErrorMessage = "Оценка за учебный год обязательна")]
        [Range(2, 5, ErrorMessage = "Оценка должна быть от 2 до 5")]
        [Display(Name = "Оценка за учебный год")]
        public double Academicyearassessment { get; set; }

        [Required(ErrorMessage = "Итоговая оценка обязательна")]
        [Range(2, 5, ErrorMessage = "Оценка должна быть от 2 до 5")]
        [Display(Name = "Итоговая оценка")]
        public double Finalassessment { get; set; }

        [Display(Name = "Школьник")]
        public int? SchoolchildrenId { get; set; }
        public SchoolchildrenModel? Schoolchildren { get; set; }
    }
}