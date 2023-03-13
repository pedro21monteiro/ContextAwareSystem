using Context_aware_System.Data;
using Context_aware_System.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//-----------------
//builder.Services.AddSingleton(typeof(Service));

builder.Services.AddSingleton<Service>();
builder.Services.AddHttpClient<IService, Service>();
//para o context database
builder.Services.AddSingleton<ContextAwareDataBaseContext>();

//Singleton para a logica do sistema
builder.Services.AddSingleton<SystemLogic>();


//----------------
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//inicializar a base de dados:
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ContextAwareDataBaseContext>();
        DbInitializer.Initalize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error ocurred while sendding the database");
    }
}

app.Run();
