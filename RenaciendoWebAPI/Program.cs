using Microsoft.EntityFrameworkCore;
using RenaciendoWebAPI.Datos;
using RenaciendoWebAPI.Mapper;
using RenaciendoWebAPI.Repositories;
using RenaciendoWebAPI.Repositories.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RenacerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RenacerCNN"));
});

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddScoped<IRenacerRepository, RenacerRepository>();

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
