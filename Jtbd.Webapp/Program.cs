using Jtbd.Application.Interfaces;
using Jtbd.Infrastructure.DataContext;
using Jtbd.Infrastructure.Repositories;
using Jtbd.Webapp.Components;
using Jtbd.Webapp.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Identity.Application"; // Or your chosen scheme
    options.DefaultSignInScheme = "Identity.External"; // If using external logins
})
.AddIdentityCookies(); // Or other authentication methods like AddOpenIdConnect, AddJwtBearer

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
builder.Services.AddScoped<IGroups, GroupsRepository>();

builder.WebHost.UseStaticWebAssets();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UsePathBase("/JTBD/");
app.UseStaticFiles();

app.Run();
