using ContextBuilder;
using ContextBuilder.Data;
using Microsoft.EntityFrameworkCore;
using Services.DataServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<DataManagement>();
builder.Services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<IContextBuilderDb, ContextBuilderDb>(options =>
{
    var dbname = System.Environment.GetEnvironmentVariable("DBNAME") ?? "ContextDb";
    var dbhost = System.Environment.GetEnvironmentVariable("DBHOST") ?? ".\\SQLEXPRESS";
    options.UseSqlServer($"Server={dbhost};Database={dbname};Trusted_Connection=True;");
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
