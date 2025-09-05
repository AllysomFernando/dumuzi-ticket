using backend.Application.Service.Implementation;
using backend.Application.Service.Interfaces;
using backend.Domain.Repository;
using backend.Infra.Persistence.PgSQL.Config;
using backend.Infra.Persistence.PgSQL.Repository;
using Microsoft.EntityFrameworkCore;
using Polly; 
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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PostgresDbContext>();

    var retryPolicy = Policy
        .Handle<Exception>()
        .WaitAndRetry(5, attempt => TimeSpan.FromSeconds(5));

    retryPolicy.Execute(() =>
    {
        Console.WriteLine("🔄 Tentando aplicar migrations...");
        db.Database.Migrate();
        Console.WriteLine("✅ Migrations aplicadas com sucesso!");
    });
}

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
            backend.Domain.Exceptions.AssertException => 400,
            backend.Domain.Exceptions.BusinessExecption => 400,
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
