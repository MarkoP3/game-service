using Asp.Versioning;
using Asp.Versioning.Conventions;
using GameService.Host.v1;
using GameService.Infrastructure.Seeders;
using GameService.Infrastructure.ApiClients;
using GameService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1.0);
    options.AssumeDefaultVersionWhenUnspecified = true;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://codechallenge.boohma.com")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});


builder.Services.AddSqlServerDb(builder.Configuration);
builder.Services.AddApiClients();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(GameService.Application.AssemblyReference.Assembly);
    config.Lifetime = ServiceLifetime.Transient;
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

var versionSet = app.NewApiVersionSet()
    .HasApiVersion(1.0)
.ReportApiVersions()
.Build();

if (app.Environment.IsDevelopment())
{
    app.MigrateAndSeedData();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.AddGameEndpointsV1(versionSet);

await app.RunAsync();

public partial class Program { protected Program() { } };