using Microservicios.Coche.Api.Extensions;
using Microservicios.Coche.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddCustomApiVersioning();
builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddCustomSwagger();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddAuthorization();

// CONFIGURACIÓN DE CORS PARA PRODUCCIÓN
builder.Services.AddCors(options => {
    options.AddPolicy("AngularDev", policy => {
        policy.AllowAnyOrigin() // Permite cualquier URL (Evita problemas en la U)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Swagger siempre activo para que el profe lo vea
app.UseSwagger();
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "swagger"; 
});

app.UseHttpsRedirection();

app.UseCors("AngularDev");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
