using Microsoft.EntityFrameworkCore;
using TaskTimePredicter.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
// (1) IMPORTAMOS LA LIBRERÍA DE LAUNCHDARKLY
using LaunchDarkly.Sdk;
using LaunchDarkly.Sdk.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr"))
    .UseLazyLoadingProxies());

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Access/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

// (2) CONFIGURACIÓN DE LAUNCHDARKLY
// ---------------------------------------------------------
var ldKey = "sdk-5a992727-cc60-446d-88b3-60b35d67756c";

var ldConfig = Configuration.Builder(ldKey).Build();
var ldClient = new LdClient(ldConfig);

// Registramos el cliente como Singleton para poder usarlo en cualquier parte de la app
builder.Services.AddSingleton<LdClient>(ldClient);
// ---------------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// (3) ENDPOINT DE PRUEBA PARA LA PRÁCTICA
// ---------------------------------------------------------
// Acceder a /test-flag en el navegador para ver esto en acción
app.MapGet("/test-flag", (LdClient client) =>
{
    // Creamos un contexto de usuario simulado para la prueba
    var user = Context.Builder("user-prueba-123").Name("Evaluador").Build();

    // Verificamos el estado de la bandera "new-algo-enabled"
    // El 'false' al final es el valor por defecto si falla la conexión
    bool showNewFeature = client.BoolVariation("new-algo-enabled", user, false);

    if (showNewFeature)
    {
        return Results.Text("[ON] FEATURE FLAG ACTIVADA: Estás viendo el Nuevo Algoritmo de Predicción.", "text/plain", System.Text.Encoding.UTF8);
    }
    else
    {
        return Results.Text("[OFF] Feature Flag Apagada: Versión Clásica Estándar.", "text/plain", System.Text.Encoding.UTF8);
    }
});
// ---------------------------------------------------------

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();