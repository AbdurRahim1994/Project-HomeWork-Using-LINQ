using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PeopleDeskHomeWork;
using PeopleDeskHomeWork.Helper;
using PeopleDeskHomeWork.Models.Data;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var _env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var isDevelopment = _env == Environments.Development;
var isProduction = _env == Environments.Production;
var isStaging = _env == Environments.Staging;

// Add services to the container.
var connectionString = string.Empty;

if (isDevelopment)
{
    connectionString = builder.Configuration.GetConnectionString("Development");
}
else if (isProduction)
{
    // connectionString = builder.Configuration.GetConnectionString("ConnectionString");
    connectionString = Environment.GetEnvironmentVariable("ConnectionString");
}
else if (isStaging)
{
    // connectionString = builder.Configuration.GetConnectionString("ConnectionString");
    connectionString = Environment.GetEnvironmentVariable("ConnectionString");
}

builder.Services.AddDbContext<HomeWorkDbContext>(options =>
options.UseSqlServer(connectionString));

Connection.iPEOPLE_HCM = connectionString;

builder.Services.AddControllersWithViews();
DependencyContainer.RegisterServices(builder.Services);
builder.Services.AddRazorPages();

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "PeopleDesk Home Work", Version = "v1" });
});
#endregion


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwagger(c =>
{
    c.RouteTemplate = "api/peopleDesk/swagger/{documentName}/swagger.json";
});

//specifying the Swagger JSON endpoint.

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/api/peopleDesk/swagger/v1/swagger.json", "PeopleDesk Home Work v1");
    c.RoutePrefix = "api/peopleDesk/swagger";
});

app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
