using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OGE.Model
{
    public class Schoolchildren : EFModel, IValidatableObject
    {
        // Скрываем унаследованное обязательное поле Name, чтобы оно не участвовало в валидации
        [BindNever]
        public new string? Name { get; set; }

        // Вычисляемое отображаемое имя (Фамилия Имя) – не сохраняется в БД
        [NotMapped]
        public string FullName => $"{Lastname} {Firstname}";

        [Required(ErrorMessage = "Имя обязательно")]
        [Display(Name = "Имя")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна")]
        [Display(Name = "Фамилия")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Возраст обязателен")]
        [Range(1, 120, ErrorMessage = "Возраст должен быть от 1 до 120")]
        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Дата рождения обязательна")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime Dateofbirthday { get; set; }

        // Проверка соответствия возраста и даты рождения
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var today = DateTime.Today;
            int calculatedAge = today.Year - Dateofbirthday.Year;
            if (Dateofbirthday.Date > today.AddYears(-calculatedAge))
                calculatedAge--;

            if (calculatedAge != Age)
            {
                yield return new ValidationResult(
                    $"Возраст ({Age}) не соответствует дате рождения ({Dateofbirthday.ToShortDateString()}). Ожидаемый возраст: {calculatedAge}.",
                    new[] { nameof(Age), nameof(Dateofbirthday) });
            }
        }
    }
}