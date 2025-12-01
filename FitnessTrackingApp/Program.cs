using FitnessTrackingApp.Components;
using FitnessTrackingApp.Components.Account;
using FitnessTrackingApp.Interfaces;
using FitnessTrackingApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FitnessTrackingApp.Data;

// Create builder for the web application
var builder = WebApplication.CreateBuilder(args);

// Fetch connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."); ;

// Configure Entity Framework with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// Add Razor components and enable interactive server components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Identity authentication setup
builder.Services.AddCascadingAuthenticationState(); // Enables authentication state cascading

builder.Services.AddScoped<IdentityUserAccessor>(); // Provides access to identity user

builder.Services.AddScoped<IdentityRedirectManager>(); // Handles redirects for identity actions

builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>(); // Revalidates identity state

// Configure authentication schemes
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme; // Main identity scheme
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme; // External login scheme
})
    .AddIdentityCookies(); // Adds default identity cookies

// Configure Identity Core with EF store and token providers
builder.Services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

// Email sender (placeholder service - does nothing)
builder.Services.AddSingleton<IEmailSender<IdentityUser>, IdentityNoOpEmailSender>();

// Workout service registration
builder.Services.AddScoped<IWorkoutService, WorkoutService>();

// Meals Service
builder.Services.AddScoped<IMealService, MealService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true); // Custom error page for production
    app.UseHsts(); // Enforce strict transport security
}

app.UseHttpsRedirection(); // Redirect HTTP to HTTPS

app.UseStaticFiles(); // Serve static files like CSS, JS, images
app.UseAntiforgery(); // Protect against CSRF

// Map Razor components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Map identity endpoints like login, logout, register
app.MapAdditionalIdentityEndpoints(); ;

// Run the application
app.Run();
