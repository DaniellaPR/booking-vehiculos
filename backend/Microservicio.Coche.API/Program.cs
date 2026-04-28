using Microservicios.Coche.Api.Extensions;
using Microservicios.Coche.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Controladores y JSON (Mantiene nombres de la BD)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Esto evita que .NET cambie las mayúsculas de tus atributos de la BD
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Configuraciones personalizadas de tus carpetas /Extensions
builder.Services.AddCustomApiVersioning();
builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddCustomSwagger();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddAuthorization();

// ── CONFIGURACIÓN DE CORS ──────────────────────────────────────────────────
// Agregamos la URL de tu frontend de Azure para que la API le de permiso
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularDev", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",   // Local HTTP
                "https://localhost:4200",  // Local HTTPS
                "https://booking-vehiculos-web.azurestaticapps.net" // <--- REEMPLAZA CON TU URL DE AZURE FRONTEND
              )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Necesario para enviar tokens JWT
    });
});

var app = builder.Build();

// Middleware global de captura de errores
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Habilitar Swagger siempre en Desarrollo (Azure tiene una variable para esto)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "swagger"; // La URL será .../swagger
    });
}

app.UseHttpsRedirection();

// EL ORDEN ES VITAL: CORS siempre antes que Auth
app.UseCors("AngularDev");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
