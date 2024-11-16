using ABSystem.Data;
using ABSystem.Data.Interfaces;
using ABSystem.Data.Models;
using ABSystem.Data.Repositories;
using ABSystem.Services.Interfaces;
using ABSystem.Services.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//Connecting to the DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ABSystemDbContext>(
    options => options.UseSqlServer(connectionString));

//Adding  Services and Repository
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ABSystemDbContext>();

        // Ensure database exists (use only for development/testing environments)
        context.Database.EnsureCreated();

        // Seed admin user
        SeedAdminUser(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred during seeding: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedAdminUser(ABSystemDbContext context)
{
    if (!context.Users.Any(u => u.Email == "demo@admin.com"))
    {
        var admin = new User
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Admin",
            LastName = "Admin",
            Email = "demo@admin.com",
            NormalizedEmail = "demo@admin.com",
            UserName = "demo@admin.com",
            NormalizedUserName = "demo@admin.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            Role = "Admin",
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            IsDeleted = 0
        };

        context.Users.Add(admin);
        context.SaveChanges();
        Console.WriteLine("Admin user seeded successfully.");
    }
    else
    {
        Console.WriteLine("Admin user already exists.");
    }
}