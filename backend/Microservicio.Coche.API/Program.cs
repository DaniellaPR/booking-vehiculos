using Microservicios.Coche.Api.Extensions;
using Microservicios.Coche.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Controladores y JSON (Mantiene nombres de la BD)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Evita que .NET convierta tus nombres de BD a minúsculas (camelCase)
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Configuraciones de tus carpetas /Extensions
builder.Services.AddCustomApiVersioning();
builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddCustomSwagger();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddAuthorization();

// ── CONFIGURACIÓN DE CORS (EL PUENTE) ──────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularDev", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200", 
                "https://localhost:4200",
                "https://black-grass-06878cb0f.7.azurestaticapps.net" // <--- ¡REEMPLAZA CON LA URL DE TU WEB DE AZURE!
              )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); 
    });
});

var app = builder.Build();

// Middleware global de captura de errores
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Habilitar Swagger siempre (útil para que el profe lo vea en la nube)
app.UseSwagger();
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "swagger"; 
});

app.UseHttpsRedirection();

// EL ORDEN ES VITAL: CORS antes que Auth
app.UseCors("AngularDev");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
