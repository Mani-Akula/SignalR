using Microsoft.EntityFrameworkCore;
using RealTimeCharts.Server.Data;
using RealTimeCharts.Server.DataStorage;
using RealTimeCharts.Server.HubConfig;
using RealTimeCharts.Server.TimerFeatures;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

builder.Services.AddSignalR();


var connectionStringBuilder = new SqlConnectionStringBuilder
{
    DataSource = "DESKTOP-ATQBQ76\\SQLEXPRESS",
    InitialCatalog = "loans",
    IntegratedSecurity = true,
    Encrypt = true,
    TrustServerCertificate = true
};

builder.Services.AddDbContext<LoansDbContext>(options =>
{
    options.UseSqlServer(connectionStringBuilder.ConnectionString);
});

builder.Services.AddSingleton<DataManager>();
builder.Services.AddSingleton<TimerManager>();

builder.Services.AddSingleton<IServiceScopeFactory>(provider =>
{
    return provider.GetService<IServiceScopeFactory>();
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChartHub>("/chart");

app.Run();
