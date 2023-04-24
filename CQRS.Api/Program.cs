using CQRS.Infrastructure.Commands;
using CQRS.Infrastructure.Commands.Handlers;
using CQRS.Infrastructure.Persistences.Contexts;
using CQRS.Infrastructure.Queries;
using CQRS.Infrastructure.Queries.Handlers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(GetAllTaskQuery).Assembly);
builder.Services.AddMediatR(typeof(GetTaskByIdQuery).Assembly);
builder.Services.AddMediatR(typeof(CreateTaskCommand).Assembly);
builder.Services.AddMediatR(typeof(UpdateTaskCommand).Assembly);
builder.Services.AddMediatR(typeof(DeleteTaskCommand).Assembly);

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<CQRSDbContext>(options =>
{
    options.UseInMemoryDatabase("TaskDb");
});




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
