using FlowerSales.WebApp.Areas.Identity.Data;
using FlowerSales.WebApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("FlowerSalesWebAppContextConnection") ?? throw new InvalidOperationException("Connection string 'FlowerSalesWebAppContextConnection' not found.");

builder.Services.AddDbContext<FlowerSalesWebAppContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<FlowerSalesWebAppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<FlowerSalesWebAppContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
