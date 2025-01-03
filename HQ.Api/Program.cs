using System.Globalization;
using HQ.Api.Filters;
using HQ.Api.Tokens;
using HQ.Application.Configurtations;
using HQ.Domain.Security.Token;
using HQ.Infra.Configurations;
using HQ.Infra.Extensions;
using HQ.Infra.Migrations;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// builder.WebHost.ConfigureKestrel(options =>
// {
//     options.ListenAnyIP(80); // Porta HTTP
//     options.ListenAnyIP(443, listenOptions => // Porta HTTPS
//     {
//         listenOptions.UseHttps("/https/cert.pfx", "certpassword");
//     });
// });


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HQ API", Version = "v1" });
    c.OperationFilter<FileUploadOperationFilter>();
});

var app = builder.Build();

// Configurar arquivos estáticos (para servir imagens da pasta "Uploads")
// Link https://localhost:7191/uploads/tampinhas.jpg
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
    RequestPath = "/uploads"
});


if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Production")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAngularApp");

MigrateDatabase();


app.Run();


void MigrateDatabase()
{
    if (builder.Configuration.IsUnitTestEnvironment())
        return;

    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    DataBaseMigration.Migrate(builder.Configuration.ConnectionString(), serviceScope.ServiceProvider);
}