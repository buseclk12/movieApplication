using BLL.DAL;
using BLL.Models;
using BLL.Services;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// PostgreSQL Configuration:
builder.Services.AddDbContext<Db>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

builder.Configuration.GetSection(nameof(AppSettings)).Bind(new AppSettings());

builder.Services.AddScoped<IService<Movies, MoviesModel>, MovieService>();
builder.Services.AddScoped<IService<Users, UsersModel>, UsersService>();
builder.Services.AddScoped<IService<Roles, RolesModel>, RolesService>();
// builder.Services.AddScoped<IService<Country, CountryModel>, CountryService>();
// builder.Services.AddScoped<IService<City, CityModel>, CityService>();
// builder.Services.AddScoped<IService<Product, ProductModel>, ProductService>();
// builder.Services.AddScoped<IService<User, UserModel>, UserService>();
// builder.Services.AddScoped<IService<Role, RoleModel>, RoleService>();
// builder.Services.AddSingleton<HttpServiceBase, HttpService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath = "/Users/Login";
        config.AccessDeniedPath = "/Users/Login";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        config.SlidingExpiration = true;
    });

builder.Services.AddSession(config =>
{
    config.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

// PostgreSQL Configuration:
app.UseAuthentication();
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Welcome}/{id?}"
);

app.Run();
