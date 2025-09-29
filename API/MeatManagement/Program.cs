using MeatManager.API.Extensions;
using MeatManager.Data;
using MeatManager.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.ConfigurePersistenceApp(builder.Configuration);
builder.Services.ConfigureServiceApp(builder.Configuration);
builder.Services.ConfigureCorsPolicy();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.SetupDatabase();

app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.Run();
