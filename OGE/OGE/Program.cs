using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using OGE.Data;
using OGE.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OGEDB")));

// Identity без стандартного UI
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Настройка путей для аутентификации
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.LoginPath = "/Account/Login";
});

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy; // все страницы требуют входа
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// Гарантированное создание администратора при каждом запуске
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    var admin = await userManager.FindByEmailAsync("admin@oge.com");
    if (admin != null)
    {
        await userManager.DeleteAsync(admin); // удаляем старого
    }

    var newAdmin = new ApplicationUser
    {
        UserName = "admin@oge.com",
        Email = "admin@oge.com",
        DisplayName = "Администратор",
        EmailConfirmed = true
    };

    var result = await userManager.CreateAsync(newAdmin, "Admin123!");
    if (result.Succeeded)
    {
        await userManager.AddToRoleAsync(newAdmin, "Admin");
    }
    else
    {
        foreach (var error in result.Errors)
            Console.WriteLine($"Admin creation error: {error.Description}");
    }
}

app.Run();