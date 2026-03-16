using DevTrack.Web.Data;
using Microsoft.EntityFrameworkCore;

// bootstrap the asp.net core app host
var builder = WebApplication.CreateBuilder(args);

// add db for part iii core data work
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// auth middleware gets added in part iii section b
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
