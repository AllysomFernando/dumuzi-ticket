using DumuziTickets.Application.Service.Implementation;
using DumuziTickets.Application.Service.Interfaces;
using DumuziTickets.Domain.Repository;
using DumuziTickets.Infra.Persistence.PgSQL.Config;
using DumuziTickets.Infra.Persistence.PgSQL.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PostgresDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IFuncionarioRepository, PgFuncionarioRepository>();
builder.Services.AddScoped<ITicketRepository, PgTicketRepository>();

builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services
    .AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(allowIntegerValues: false));
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Development", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        context.Response.StatusCode = exception switch
        {
            DbUpdateException => 400,
            KeyNotFoundException => 404,
            DumuziTickets.Domain.Exceptions.AssertException => 400,
            DumuziTickets.Domain.Exceptions.BusinessExecption => 400,
            ArgumentException => 400,
            _ => 500
        };

        await context.Response.WriteAsJsonAsync(new
        {
            error = exception?.Message
        });
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("Development");

app.MapControllers();
app.Run();
