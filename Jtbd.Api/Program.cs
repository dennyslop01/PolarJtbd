using Jtbd.Application.Interfaces;
using Jtbd.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Jtbd.Infrastructure.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.File("logs/JtbdApi-.log", rollingInterval: RollingInterval.Day)); // Log to file

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<JtbdDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn")));

builder.Services.AddScoped<IAnxieties, AnxietiesRepository>();
builder.Services.AddScoped<ICategories, CategoriesRepository>();
builder.Services.AddScoped<IDeparments, DeparmentsRepository>();
builder.Services.AddScoped<IEmployee, EmployeeRepository>();
builder.Services.AddScoped<IHabits, HabitsRepository>();
builder.Services.AddScoped<IInterviews, InterviewsRepository>();
builder.Services.AddScoped<IProjects, ProjectsRepository>();
builder.Services.AddScoped<IPullGroups, PullGroupsRepository>();
builder.Services.AddScoped<IPushesGroups, PushesGroupsRepository>();
builder.Services.AddScoped<IStories, StoriesRepository>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
