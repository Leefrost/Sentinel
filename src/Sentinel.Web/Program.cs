using BlazorBlueprint.Components;
using Sentinel.Web;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost
    .ConfigureKestrel(options => options.AddServerHeader = false);

builder.Host.UseDefaultServiceProvider((_, options) =>
{
    options.ValidateOnBuild = true;
    options.ValidateScopes = true;
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazorBlueprintComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseSecurityHeaders();

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
