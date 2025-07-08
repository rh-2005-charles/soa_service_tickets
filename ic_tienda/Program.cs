using System.Text;
using ic_tienda.Infrastructure;
using ic_tienda_data.sources.BaseDeDatos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

// Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ic_tienda API", Version = "v1" });

    // Configurar autenticación con Bearer Token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingrese el token JWT en este formato: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference=new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
}

);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMyServices(builder.Configuration);
//builder.Services.AddMyServices();


// Para MySQL:
builder.Services.AddDbContext<IcTiendaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DbCloudMySQL"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DbCloudMySQL"))));


// Leer orígenes permitidos desde appsettings.json
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(allowedOrigins) // Frontend URL
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure JWT Authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"]
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
//app.Run();

// Habilitar la entrega de archivos estáticos desde wwwroot
app.UseStaticFiles();
app.UseCors("AllowFrontend");

// Middleware

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();