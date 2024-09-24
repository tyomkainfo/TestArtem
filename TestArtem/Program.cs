using ClientServices;
using ClientServices.WebApi;
using TestArtem.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(); // Добавляем поддержку Razor Pages
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Указываем маршрут по умолчанию для страницы логина
app.MapGet("/", (context) =>
{
    context.Response.Redirect("/login"); // Теперь перенаправляем на /login
    return Task.CompletedTask;
});

// Запуск приложения
app.Run();
