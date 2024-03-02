using Portfolio.Domain;
using Portfolio.Data.Context;
using Portfolio.CrossCutting.DependencyInjection;
using Portfolio.CrossCutting.Mappings;
using Portfolio.Application.ErrorHandling;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Certificate;

var builder = WebApplication.CreateBuilder(args);

if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddMassTransitRabbitMQ(
        Environment.GetEnvironmentVariable("RABBITMQ_HOST"),
        Environment.GetEnvironmentVariable("RABBITMQ_USERNAME"),
        Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD")
    );
}

// Add services to the container.

builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCorsConfiguration();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddJwtConfiguration(builder.Configuration, builder.Environment);

builder.Services.AddSwaggerConfiguration();

builder.Services.AddIdentityInfrastructure(builder.Configuration, builder.Environment);

builder.Services.AddFluentValidationConfiguration();

builder.Services.AddMediatRConfiguration();

var mapperConfig = MapperProfile.Configure();

// Configurando Mapper
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Filtro de logs
builder.Services.AddLogging(logging =>
{
    logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
});

var app = builder.Build();

app.UseCors();

app.UseHsts();

app.UseRouting();

app.UseStaticFiles();

app.UseJwtConfiguration();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

if (app.Environment.IsEnvironment("Staging") || app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerConfiguration();
}

app.UseMiddleware(typeof(ErrorHandlingMiddleware));

if (builder.Configuration.GetValue<bool>("Migrations:Apply"))
{
    using var service = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    using var context = service.ServiceProvider.GetService<PortfolioContext>();
    context.Database.Migrate();
}

app.UseAuthorization();

app.MapControllers();

app.UseAuthentication();

app.Run();