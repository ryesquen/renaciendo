using Microsoft.EntityFrameworkCore;
using OracleWebAPI.Data.Models;
using OracleWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cn = builder.Configuration.GetConnectionString("OracleConnection");

builder.Services.AddTransient<ICategoriaService, CategoriaService>();

builder.Services.AddDbContext<ModelContext>(opt =>
    opt.UseOracle(
        cn,
        options => options.UseOracleSQLCompatibility("11")
    )
);


var app = builder.Build();

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
