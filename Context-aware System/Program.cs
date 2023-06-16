using System.Xml.Linq;
using ContextServer.Data;
using ContextServer.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//-----------------
//builder.Services.AddSingleton(typeof(Service));

//builder.Services.AddSingleton<Service>();
//builder.Services.AddHttpClient<IService, Service>();
//para o context database
//builder.Services.AddSingleton<ContextAwareDb>();
builder.Services.AddDbContext<ContextAwareDb>(options =>
{
    var dbname = System.Environment.GetEnvironmentVariable("DBNAME") ?? "ContextDb";
    var dbhost = System.Environment.GetEnvironmentVariable("DBHOST") ?? "192.168.28.86";
    var dbuser = System.Environment.GetEnvironmentVariable("DBUSER") ?? "sa";
    var dbpass = System.Environment.GetEnvironmentVariable("DBPASS") ?? "xA6UCjFY";
    options.UseSqlServer("Data Source=" + dbhost + $";Database={dbname};User ID=" + dbuser + ";Password=" + dbpass + ";TrustServerCertificate=Yes;");
});

//Singleton para a logica do sistema
builder.Services.AddSingleton<SystemLogic>();


//----------------
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();

app.Run();
