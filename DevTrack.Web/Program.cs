// Bootstrap the ASP.NET Core application host.
var builder = WebApplication.CreateBuilder(args);

// Register MVC services (controllers + views).
// Placeholder for Part III:
// - Add DbContext for EF Core
// - Add ASP.NET Identity services for authentication/authorization
// - Add typed HttpClient for GitHub API integration
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure middleware pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // In production, force HTTPS with HSTS.
    // Review HSTS duration/settings before final deployment.
    app.UseHsts();
}

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();
// Enable endpoint routing.
app.UseRouting();

// Placeholder for Part III:
// app.UseAuthentication();
// Add authentication middleware once login/roles are configured.
app.UseAuthorization();

// Serve static files (css/js/images) and map static asset endpoints.
app.MapStaticAssets();

// Default MVC route for the application shell.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Start handling incoming requests.
app.Run();
