using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OGE.Data;
using OGE.Model;
using Xunit;

namespace OGE.Tests
{
    public class ApplicationDbContextMoqTests
    {
        [Fact]
        public async Task AddSchoolchild_SavesToDatabase()
        {
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_AddSchoolchild")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var student = new Schoolchildren
                {
                    Firstname = "Анна",
                    Lastname = "Сидорова",
                    Age = 16,
                    Email = "anna@example.com",
                    Dateofbirthday = new DateTime(2008, 1, 15)
                };

                
                context.Schoolchildren.Add(student);
                await context.SaveChangesAsync();
            }

            
            using (var context = new ApplicationDbContext(options))
            {
                var savedStudent = await context.Schoolchildren.FirstOrDefaultAsync(s => s.Firstname == "Анна");
                Assert.NotNull(savedStudent);
                Assert.Equal("Сидорова", savedStudent.Lastname);
            }
        }
    }
}