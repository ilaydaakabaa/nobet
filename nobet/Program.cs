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

// Veritabaný baðlantýsýný yapýlandýr
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Burada baðlantý dizesini ekliyoruz

// Servisleri ekle
builder.Services.AddControllersWithViews();

// Kimlik doðrulama ve oturum yönetimi için gerekli hizmetleri ekle
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login"; // Giriþ sayfasýnýn yolu
        options.LogoutPath = "/User/Logout"; // Çýkýþ sayfasýnýn yolu
        options.AccessDeniedPath = "/User/AccessDenied"; // Eriþim reddedildi sayfasý
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

// Kimlik doðrulama ve yetkilendirme middleware'lerini ekle
app.UseAuthentication();
app.UseAuthorization();

// Kullanýcý oluþturma iþlemi
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate(); // Veritabaný migrasyonlarýný uygula

    // Admin kullanýcýsýný oluþturma
    if (!context.Users.Any(u => u.Email == "admin@example.com"))
    {
        var adminUser = new User
        {
            Email = "admin@example.com",
            Password = "admin123", // Þifreyi düz metin olarak sakla
            Role = UserRole.Admin
        };

        context.Users.Add(adminUser);
        await context.SaveChangesAsync(); // Deðiþiklikleri kaydet
    }
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
