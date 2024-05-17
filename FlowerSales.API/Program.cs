using Asp.Versioning;
using FlowerSales.API.Models;
using FlowerSales.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add API versioning
builder.Services
	.AddApiVersioning(options =>
	{
		options.ReportApiVersions = true;
		options.DefaultApiVersion = new ApiVersion(1, 0);
		options.AssumeDefaultVersionWhenUnspecified = true;

		options.ApiVersionReader = new HeaderApiVersionReader("CITEMS-API-Version");
		// options.ApiVersionReader = new QueryStringApiVersionReader("CITEMS-API-Version");
	})
	.AddApiExplorer(options =>
	{
		options.GroupNameFormat = "'v'VVV"; // v1
		options.SubstituteApiVersionInUrl = true;
	});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add FlowerSalesDatabaseSettings
builder.Services.Configure<FlowerSalesDatabaseSettings>(
	builder.Configuration.GetSection("FlowerSalesDatabase"));

// Add FlowerService
builder.Services.AddSingleton<FlowersService>();

// Add CORS
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		builder
			.WithOrigins("https://localhost:7283")
			.WithHeaders("CITEMS-API-Version");
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
// Enforce HTTPS
else
{
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Use CORS
app.UseCors();

app.MapControllers();

app.Run();
