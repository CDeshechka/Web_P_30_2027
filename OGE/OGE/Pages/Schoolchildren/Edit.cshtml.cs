using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OGE.Data;
using SchoolchildrenModel = OGE.Model.Schoolchildren;  // ? псевдоним
using System;
using System.Threading.Tasks;

namespace OGE.Pages.Schoolchildren
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SchoolchildrenModel Schoolchild { get; set; }   // ? SchoolchildrenModel

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Schoolchild = await _context.Schoolchildren.FirstOrDefaultAsync(s => s.Id == id);

            if (Schoolchild == null)
            {
                return NotFound();
            }

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

            _context.Attach(Schoolchild).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolchildExists(Schoolchild.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SchoolchildExists(int id)
        {
            return _context.Schoolchildren.Any(e => e.Id == id);
        }
    }
}