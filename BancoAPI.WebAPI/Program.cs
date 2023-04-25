using BancoAPI.Application;
using BancoAPI.Persistence;
using BancoAPI.Shared;
using BancoAPI.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationLayer();
builder.Services.AddShareInfrastructure();
builder.Services.AddPersintenceInfrastructure(builder.Configuration);

builder.Services.AddApiVersionExtensions();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseErrorHandlingMiddelware();

app.MapControllers();

app.Run();
