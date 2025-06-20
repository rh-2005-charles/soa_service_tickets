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

    // CustomerAuth
    builder.AddService<CustomerAuthServiceSOAP>(sOpt =>
    {
        sOpt.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    builder.AddServiceEndpoint<CustomerAuthServiceSOAP, ICustomerAuthServiceSOAP>(eventBinding, "/CustomerAuthService.svc");

    // Event
    builder.AddService<EventServiceSOAP>(sOpt =>
    {
        sOpt.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    builder.AddServiceEndpoint<EventServiceSOAP, IEventServiceSOAP>(eventBinding, "/EventService.svc");

    // TycketType
    builder.AddService<TicketTypeServiceSOAP>(sOpt =>
    {
        sOpt.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    builder.AddServiceEndpoint<TicketTypeServiceSOAP, ITicketTypeServiceSOAP>(eventBinding, "/TicketTypeService.svc");

    // Tycket
    builder.AddService<TicketServiceSOAP>(sOpt =>
    {
        sOpt.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    builder.AddServiceEndpoint<TicketServiceSOAP, ITicketServiceSOAP>(eventBinding, "/TicketService.svc");

    // Order
    builder.AddService<OrderServiceSOAP>(sOpt =>
    {
        sOpt.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    builder.AddServiceEndpoint<OrderServiceSOAP, IOrderServiceSOAP>(eventBinding, "/OrderService.svc");

    // OrderDetail
    builder.AddService<OrderDetailServiceSOAP>(sOpt =>
    {
        sOpt.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    builder.AddServiceEndpoint<OrderDetailServiceSOAP, IOrderDetailServiceSOAP>(eventBinding, "/OrderDetailService.svc");


    // Habilitar metadata WSDL
    var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
    serviceMetadataBehavior.HttpGetEnabled = true;
});

app.Run();
