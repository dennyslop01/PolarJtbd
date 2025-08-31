using Jtbd.Webapp.Components;
using Jtbd.Webapp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

string apiBack = builder.Configuration.GetSection("ApiBack").Value.ToString();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBack) });
builder.Services.AddScoped<IRepositoryGeneric, RepositoryGeneric>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
