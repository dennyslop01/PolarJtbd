using Jtbd.Application.Interfaces;
using Jtbd.Infrastructure.DataContext;
using Jtbd.Infrastructure.Repositories;
using Jtbd.Webapp.Components;
using Jtbd.Webapp.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Identity.Application";
    options.DefaultSignInScheme = "Identity.External";
})
.AddIdentityCookies();

// Configure DbContext as Transient to avoid Blazor Server concurrency collisions on the same circuit
builder.Services.AddDbContext<JtbdDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}, ServiceLifetime.Transient, ServiceLifetime.Transient);

builder.Services.AddDbContextFactory<JtbdDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Repositories registered as Transient so each gets its own independent DbContext instance
builder.Services.AddTransient<IAnxieties, AnxietiesRepository>();
builder.Services.AddTransient<ICategories, CategoriesRepository>();
builder.Services.AddTransient<IDeparments, DeparmentsRepository>();
builder.Services.AddTransient<IEmployee, EmployeeRepository>();
builder.Services.AddTransient<IHabits, HabitsRepository>();
builder.Services.AddTransient<IInterviews, InterviewsRepository>();
builder.Services.AddTransient<IProjects, ProjectsRepository>();
builder.Services.AddTransient<IPullGroups, PullGroupsRepository>();
builder.Services.AddTransient<IPushesGroups, PushesGroupsRepository>();
builder.Services.AddTransient<IStories, StoriesRepository>();
builder.Services.AddTransient<IGroups, GroupsRepository>();
builder.Services.AddTransient<IMatrizWard, MatrizWardRepository>();
builder.Services.AddMudServices();
builder.WebHost.UseStaticWebAssets();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseAntiforgery();
app.UseAuthentication();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.UseSession();
app.UsePathBase("/JTBD/");
app.UseStaticFiles();

app.Run();
