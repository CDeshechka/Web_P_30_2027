using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OGE.Data;
using OGE.Model;
using OGE.Pages.Schoolchildren;
using Xunit;

namespace OGE.Tests
{
    public class SchoolchildrenIndexPageTests
    {
        [Fact]
        public async Task OnGetAsync_ReturnsPageWithSchoolchildrenList()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Index")
                .Options;

            using var context = new ApplicationDbContext(options);
            context.Schoolchildren.Add(new Schoolchildren
            {
                Firstname = "Пётр",
                Lastname = "Петров",
                Age = 17,
                Email = "petr@example.com",
                Dateofbirthday = new DateTime(2007, 5, 10)
            });
            context.SaveChanges();

            var pageModel = new IndexModel(context);

            await pageModel.OnGetAsync();

            Assert.NotNull(pageModel.SchoolchildrenList);
            Assert.Single(pageModel.SchoolchildrenList);
            Assert.Equal("Петров", pageModel.SchoolchildrenList[0].Lastname);
        }
    }
}