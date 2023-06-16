using System.Xml.Linq;
using ContinentalTestAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContinentalDb>(options =>
{
    var dbname = System.Environment.GetEnvironmentVariable("DBNAME") ?? "ContinentalTestDb";
    var dbhost = System.Environment.GetEnvironmentVariable("DBHOST") ?? "192.168.28.86";
    var dbuser = System.Environment.GetEnvironmentVariable("DBUSER") ?? "sa";
    var dbpass = System.Environment.GetEnvironmentVariable("DBPASS") ?? "xA6UCjFY";
    options.UseSqlServer("Data Source=" + dbhost + $";Database={dbname};User ID=" + dbuser + ";Password=" + dbpass + ";TrustServerCertificate=Yes;");
});

var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
