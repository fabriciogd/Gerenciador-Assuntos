using Microsoft.EntityFrameworkCore;
using Serilog;
using Topic.Application;
using Topic.BackgroundTasks;
using Topic.Infraestructure;
using Topic.Persistence;
using Topic.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddControllers();

builder.Services
    .AddHttpContextAccessor()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(x => x.EnableAnnotations());

builder.Services
    .AddCommandHandlers()
    .AddPersistence(builder.Configuration)
    .AddServices(builder.Configuration)
    .AddBackgroundTasks(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TopicDbContext>();
    db.Database.Migrate();
}

app.Run();
