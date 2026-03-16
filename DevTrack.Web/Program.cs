using DevTrack.Web.Data;
using DevTrack.Web.Models;
using DevTrack.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// bootstrap the asp.net core app host
var builder = WebApplication.CreateBuilder(args);

// add db for part iii core data work
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// add identity for login/register/roles
builder.Services
    .AddIdentity<AppUser, IdentityRole>(options =>
    {
        // keep password rules simple for class project testing
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// cookie paths for auth flow
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// typed client for github repo metadata sync
builder.Services.AddHttpClient<IGitHubRepoService, GitHubRepoService>(client =>
{
    client.BaseAddress = new Uri("https://api.github.com/");
    client.DefaultRequestHeaders.UserAgent.ParseAdd("DevTrackApp/1.0");
    client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");
});

// register mvc services (controllers + views)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// configure middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // in production, force https with hsts
    app.UseHsts();
}

// redirect http requests to https
app.UseHttpsRedirection();
// enable endpoint routing
app.UseRouting();

// enable auth before authorization checks
app.UseAuthentication();
app.UseAuthorization();

// serve static files (css/js/images) and map static asset endpoints
app.MapStaticAssets();

// default mvc route for the app shell
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// create db + seed starter data for dev
await DbSeeder.SeedCoreDataAsync(app.Services);

// start handling incoming requests
app.Run();
