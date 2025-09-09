using Jtbd.Application.Interfaces;
using Jtbd.Infrastructure.DataContext;
using Jtbd.Infrastructure.Repositories;
using Jtbd.Webapp.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDistributedMemoryCache(); // Or another distributed cache provider
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(300); // Customize session timeout
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<JtbdDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddHttpContextAccessor();

//string apiBack = builder.Configuration.GetSection("ApiBack").Value.ToString();
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBack) });

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

builder.WebHost.UseStaticWebAssets();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
//app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UsePathBase("/JTBD/");
app.UseStaticFiles();

app.Run();
