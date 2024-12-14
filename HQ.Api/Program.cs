using System.Globalization;
using HQ.Api.Filters;
using HQ.Application.Configurtations;
using HQ.Infra.Configurations;
using HQ.Infra.Extensions;
using HQ.Infra.Migrations;

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
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));


builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Production")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();


app.Run();



void MigrateDatabase()
{
    if (builder.Configuration.IsUnitTestEnvironment())
        return;

    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    DataBaseMigration.Migrate(builder.Configuration.ConnectionString(), serviceScope.ServiceProvider);
}