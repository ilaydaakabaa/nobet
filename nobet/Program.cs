using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using emergencyProject.DataDb;
using emergencyProject.Models;
using Microsoft.EntityFrameworkCore;
using nobet.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant�s�n� yap�land�r
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Burada ba�lant� dizesini ekliyoruz

// Servisleri ekle
builder.Services.AddControllersWithViews();

// Kimlik do�rulama ve oturum y�netimi i�in gerekli hizmetleri ekle
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login"; // Giri� sayfas�n�n yolu
        options.LogoutPath = "/User/Logout"; // ��k�� sayfas�n�n yolu
        options.AccessDeniedPath = "/User/AccessDenied"; // Eri�im reddedildi sayfas�
    });

var app = builder.Build();

// Middleware'leri ekle
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Kimlik do�rulama ve yetkilendirme middleware'lerini ekle
app.UseAuthentication();
app.UseAuthorization();

// Kullan�c� olu�turma i�lemi
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate(); // Veritaban� migrasyonlar�n� uygula

    // Admin kullan�c�s�n� olu�turma
    if (!context.Users.Any(u => u.Email == "admin@example.com"))
    {
        var adminUser = new User
        {
            Email = "admin@example.com",
            Password = "admin123", // �ifreyi d�z metin olarak sakla
            Role = UserRole.Admin
        };

        context.Users.Add(adminUser);
        await context.SaveChangesAsync(); // De�i�iklikleri kaydet
    }
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
