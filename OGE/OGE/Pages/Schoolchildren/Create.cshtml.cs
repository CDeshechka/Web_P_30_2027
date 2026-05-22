using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OGE.Data;
using SchoolchildrenModel = OGE.Model.Schoolchildren;  // ? псевдоним
using System;
using System.Threading.Tasks;

namespace OGE.Pages.Schoolchildren
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SchoolchildrenModel Schoolchild { get; set; }   // ? SchoolchildrenModel

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Проверка возраста и даты рождения
            if (Schoolchild.Dateofbirthday != default)
            {
                var today = DateTime.Today;
                int calculatedAge = today.Year - Schoolchild.Dateofbirthday.Year;
                if (Schoolchild.Dateofbirthday.Date > today.AddYears(-calculatedAge))
                    calculatedAge--;

                if (calculatedAge != Schoolchild.Age)
                {
                    ModelState.AddModelError("Schoolchild.Age",
                        $"Возраст ({Schoolchild.Age}) не соответствует дате рождения ({Schoolchild.Dateofbirthday.ToShortDateString()}). Ожидаемый возраст: {calculatedAge}.");
                    ModelState.AddModelError("Schoolchild.Dateofbirthday",
                        $"Дата рождения не соответствует указанному возрасту ({Schoolchild.Age}).");
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Schoolchildren.Add(Schoolchild);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}