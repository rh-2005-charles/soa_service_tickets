using System.Text;
using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Configuration;
using CoreWCF.Description;
using ic_tienda.Contracts;
using ic_tienda.Infrastructure;
using ic_tienda.Services;
using ic_tienda_data.sources.BaseDeDatos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Configuración base
builder.Services.AddHttpContextAccessor();
builder.Services.AddMyServices(builder.Configuration);

// Configuración de la base de datos MySQL
builder.Services.AddDbContext<IcTiendaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DbCloudMySQL"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DbCloudMySQL"))));

// Configuración JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
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

builder.Services.AddAuthorization();

// Configuración SOAP con CoreWCF
builder.Services.AddServiceModelServices()
    .AddServiceModelMetadata()
    .AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();



var app = builder.Build();

//app.UseMiddleware<ErrorMiddleware>();
app.UseAuthentication();
app.UseAuthorization();


// Configuración del endpoint SOAP
app.UseServiceModel(builder =>
{
    var eventBinding = new BasicHttpBinding
    {
        MaxReceivedMessageSize = int.MaxValue,
        ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas
        {
            MaxDepth = 32,
            MaxArrayLength = int.MaxValue,
            MaxStringContentLength = int.MaxValue
        },
        Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.None },
        CloseTimeout = TimeSpan.FromMinutes(1),
        OpenTimeout = TimeSpan.FromMinutes(1),
        ReceiveTimeout = TimeSpan.FromMinutes(10),
        SendTimeout = TimeSpan.FromMinutes(1)
    };

    builder.AddService<CustomerAuthServiceSOAP>(serviceOptions =>
    {
        // Habilitar detalles de error
        serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    builder.AddServiceEndpoint<CustomerAuthServiceSOAP, ICustomerAuthServiceSOAP>(
       eventBinding, "/CustomerAuthService.svc");

    builder.AddService<EventServiceSOAP>(serviceOptions =>
    {
        serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    builder.AddServiceEndpoint<EventServiceSOAP, IEventServiceSOAP>(
       eventBinding, "/EventService.svc");

    // Habilitar metadata WSDL
    var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
    serviceMetadataBehavior.HttpGetEnabled = true;
});

app.Run();
