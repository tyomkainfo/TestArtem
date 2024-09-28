using ClientServices;
using ClientServices.WebApi;
using TestArtem.Components;
using TestArtem.Components.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<AuthService>();

builder.Services.AddSingleton<IWebApiBestellungen,WebApiBestellungen>();
builder.Services.AddSingleton<BestellungenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();


app.MapGet("/", (context) =>
{
    context.Response.Redirect("/login");
    return Task.CompletedTask;
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();
