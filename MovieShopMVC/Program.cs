using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servies;
using ApplicationCore.Entities;
using Infrastructure.Services;
using Intrastructure.Data;
using Intrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MovieShopMVC.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add repositories to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICastRepository, CastRepository>();
builder.Services.AddScoped<IRepository<Genre>, EfRepository<Genre>>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add services to the container.
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICastService, CastService>();
builder.Services.AddScoped<ICurrentLoginUserService, CurentLoginUserService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddHttpContextAccessor();

// Cookie based authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "MovieShopAuthCookie";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.LoginPath = "/account/login"; // after cookie is expired, redirect to login page
    });

// Inject connection string to DbContext
builder.Services.AddDbContext<MovieShopDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MovieShopDbConnection"));
    }
);

var app = builder.Build();

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

// Middlewares in ASP.NET Core  
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();