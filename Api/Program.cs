using Carter;
using Microsoft.EntityFrameworkCore;
using Api.Database;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURACIÓN DE SERVICIOS (DI CONTAINER)

// A. Servicio de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            //policy.WithOrigins("http://localhost:5173") 
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// B. Configuración de Swagger (Documentación API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// C. Configuración de Base de Datos (Entity Framework Core)
// Lee la "ConnectionStrings:DefaultConnection" del archivo appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// D. Registrar MediatR (Escanea todo el ensamblado actual buscando Handlers)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// E. Registrar Carter (Escanea los módulos ICarterModule para los Endpoints REPR)
builder.Services.AddCarter();

// ==========================================
// 2. CONSTRUCCIÓN DE LA APP
// ==========================================
var app = builder.Build();

// ==========================================
// 3. PIPELINE DE PETICIONES HTTP
// ==========================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.MapCarter();

app.Run();