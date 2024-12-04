using ABSystem.Data;
using ABSystem.Data.Interfaces;
using ABSystem.Data.Models;
using ABSystem.Data.Repositories;
using ABSystem.Services.Interfaces;
using ABSystem.Services.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ABSystem.WebApp;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//Connecting to the DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ABSystemDbContext>(
    options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>(
    options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
    }
    )
    .AddEntityFrameworkStores<ABSystemDbContext>().AddDefaultTokenProviders();

builder.Services.AddDistributedMemoryCache();  // Use in-memory cache for sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Set session timeout
    options.Cookie.HttpOnly = true; // Set HttpOnly flag for session cookie
    options.Cookie.IsEssential = true;  // Mark cookie as essential
});

//Adding  Services and Repository
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/");
    options.AccessDeniedPath = new PathString("/");
    //other properties
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = null;
});



var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ABSystemDbContext>();

        // Ensure database exists (use only for development/testing environments)
        //context.Database.EnsureCreated();

        // Seed admin user
        Seeder.Seed(context);
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
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(@"C:\ABSystem\Rooms"),
    RequestPath = "/Rooms"
});

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "register",
    pattern: "register",
    defaults: new { controller = "Account", action = "Register" });


app.Run();

/*void SeedAdminUser(ABSystemDbContext context)
{

    // Check if the 'Admin' role exists
    if (!context.Roles.Any(r => r.Name == "Admin"))
    {
        var adminRole = new IdentityRole
        {
            Name = "Admin",
            NormalizedName = "ADMIN"
        };
        context.Roles.Add(adminRole);
        context.SaveChanges();
        Console.WriteLine("Admin role seeded successfully.");
    }


    // Check if the 'SuperAdmin' role exists
    if (!context.Roles.Any(r => r.Name == "SuperAdmin"))
    {
        var superAdminRole = new IdentityRole
        {
            Name = "SuperAdmin",
            NormalizedName = "SUPERADMIN"
        };
        context.Roles.Add(superAdminRole);
        context.SaveChanges();
        Console.WriteLine("SuperAdmin role seeded successfully.");
    }

    if (!context.Roles.Any(r => r.Name == "User"))
    {
        var superAdminRole = new IdentityRole
        {
            Name = "User",
            NormalizedName = "USER"
        };
        context.Roles.Add(superAdminRole);
        context.SaveChanges();
        Console.WriteLine("User role seeded successfully.");
    }


    // Check if the admin user exists
    if (!context.Users.Any(u => u.Email == "demo@admin.com"))
    {
        var passwordHasher = new PasswordHasher<User>();

        var admin = new User
        {
            FirstName = "Admin",
            LastName = "Admin",
            Email = "demo@admin.com",
            NormalizedEmail = "DEMO@ADMIN.COM",
            UserName = "demo@admin.com",
            NormalizedUserName = "DEMO@ADMIN.COM",
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            IsDeleted = 0
        };

        admin.PasswordHash = passwordHasher.HashPassword(admin, "12345678");

        context.Users.Add(admin);
        context.SaveChanges();
        Console.WriteLine("Admin user seeded successfully.");

        // Assign the 'Admin' role to the newly created admin user
        var adminRoleId = context.Roles.First(r => r.Name == "Admin").Id;
        var adminUserId = context.Users.First(u => u.Email == "demo@admin.com").Id;

        context.UserRoles.Add(new IdentityUserRole<string>
        {
            UserId = adminUserId,
            RoleId = adminRoleId
        });
        context.SaveChanges();
        Console.WriteLine("Admin user assigned to Admin role successfully.");
    }

    // Check if the SuperAdmin user exists
    if (!context.Users.Any(u => u.Email == "superadmin@admin.com"))
    {
        var passwordHasher = new PasswordHasher<User>();

        var superAdmin = new User
        {
            FirstName = "SuperAdmin",
            LastName = "SuperAdmin",
            Email = "demo@superadmin.com",
            NormalizedEmail = "DEMO@SUPERADMIN.COM",
            UserName = "demo@superadmin.com",
            NormalizedUserName = "DEMO@SUPERADMIN.COM",
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            IsDeleted = 0
        };

        superAdmin.PasswordHash = passwordHasher.HashPassword(superAdmin, "12345678");

        context.Users.Add(superAdmin);
        context.SaveChanges();
        Console.WriteLine("SuperAdmin user seeded successfully.");

        // Assign the 'SuperAdmin' role to the newly created SuperAdmin user
        var superAdminRoleId = context.Roles.First(r => r.Name == "SuperAdmin").Id;
        var superAdminUserId = context.Users.First(u => u.Email == "demo@superadmin.com").Id;

        context.UserRoles.Add(new IdentityUserRole<string>
        {
            UserId = superAdminUserId,
            RoleId = superAdminRoleId
        });
        context.SaveChanges();
        Console.WriteLine("SuperAdmin user assigned to SuperAdmin role successfully.");
    }

    if (!context.Users.Any(u => u.Email == "demo@user.com"))
    {
        var passwordHasher = new PasswordHasher<User>();

        var user = new User
        {
            FirstName = "User",
            LastName = "User",
            Email = "demo@user.com",
            NormalizedEmail = "DEMO@USER.COM",
            UserName = "demo@user.com",
            NormalizedUserName = "DEMO@USER.COM",
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            IsDeleted = 0
        };

        user.PasswordHash = passwordHasher.HashPassword(user, "12345678");

        context.Users.Add(user);
        context.SaveChanges();
        Console.WriteLine("User user seeded successfully.");

        // Assign the 'User' role to the newly created User user
        var userRoleId = context.Roles.First(r => r.Name == "User").Id;
        var userUserId = context.Users.First(u => u.Email == "demo@user.com").Id;

        context.UserRoles.Add(new IdentityUserRole<string>
        {
            UserId = userUserId,
            RoleId = userRoleId
        });
        context.SaveChanges();
        Console.WriteLine("User assigned to User role successfully.");
    }
}*/