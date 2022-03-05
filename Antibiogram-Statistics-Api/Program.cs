using MedicalSystems.Antibiogram.StatisticsApi.Services;
using MedicalSystems.Antibiogram.StatisticsApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("AntibiogramDatabase"));
var EnviromentConnectionString = builder.Configuration.GetValue<string>("connection-string");
if (!string.IsNullOrEmpty(EnviromentConnectionString))
{
	builder.Configuration["AntibiogramDatabase:ConnectionString"] = EnviromentConnectionString;
}

builder.Services.AddSingleton<PatientsService>();
builder.Services.AddSingleton<LabsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
