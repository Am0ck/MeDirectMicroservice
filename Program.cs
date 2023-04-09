using MeDirectMicroservice.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MeDirectMicroservice.DataAccess.Interfaces;
using MeDirectMicroservice.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);
var TradingApiKey = builder.Configuration["ServiceApiKey"];

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<ITradeRepository, TradeRepository>();
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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ExchangeTrades}/{action=Create}");
app.MapRazorPages();

app.Run();
