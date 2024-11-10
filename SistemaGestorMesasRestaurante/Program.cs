using SistemaGestorMesasRestaurante.Data;
using DemoDeIdentity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // Habilita controladores con vistas
builder.Services.AddRazorPages();

// Configuración de la conexión a la base de datos para el contexto del sistema de gestión.
builder.Services.AddDbContext<SistemaGestionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));

// Configuración de la conexión a la base de datos para el contexto de identidad.
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyIdentityDBContext>(o =>
    o.UseSqlServer(conn));

// Configuración de autenticación y autorización.
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

builder.Services
    .AddIdentity<MyUser, MyRol>(options =>
    {
        // Configuración de la contraseña
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 8;

        // Confirmación de correo
        options.SignIn.RequireConfirmedEmail = false;

        // Configuración de bloqueo
        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.MaxFailedAccessAttempts = 8;
    })
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<MyIdentityDBContext>()
    .AddApiEndpoints();

// Configuración de cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
});

// Configuración para Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración de la API de identidad personalizada
app.MapIdentityApi<MyUser>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Cambiar según las rutas de error definidas
    app.UseHsts(); // Valor HSTS predeterminado de 30 días
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redirección HTTPS
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Ruta para controladores MVC

app.Run();
