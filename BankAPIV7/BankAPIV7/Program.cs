using BankAPIV7.Services;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
// Add services to the container.
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.Configure<LdapConfig>(configuration.GetSection("Ldap"));
builder.Services.AddScoped<ILdapService, LdapService>();
//Log.Logger = new LoggerConfiguration()
//    .Enrich.FromLogContext()

//    .WriteTo.Debug()
//    .WriteTo.Console()
//    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new
//        Uri(configuration["ElasticConfiguration:Uri"]))
//    {
//        AutoRegisterTemplate = true,
//        IndexFormat = $"VSecureIndex-{DateTime.UtcNow:yyyy-MM}"
//    })
//    .Enrich.WithProperty("Environment", environment)
//    .ReadFrom.Configuration(configuration)
//    .CreateLogger();

//builder.Host.UseSerilog();

//Retry Policy

builder.Services.AddHttpClient("WeatherClient", c =>
{
    c.BaseAddress = new Uri("http://localhost:7072/");
}).AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(new[]
{
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(15),
                 TimeSpan.FromSeconds(15)
            }))
.AddTransientHttpErrorPolicy(policy => 
policy.FallbackAsync(new System.Net.Http.HttpResponseMessage
(System.Net.HttpStatusCode.RequestTimeout)));


//Circuit Breaker Policy
//circuit opens up after 2 consecutive trials

//builder.Services.AddHttpClient("cartApiClient", c => {
//    c.BaseAddress =
//new Uri("http://localhost:5097");
//})
//.AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(2, TimeSpan.FromMinutes(2)));


//Bulkhead Policy

builder.Services.AddSingleton<Polly.Bulkhead.AsyncBulkheadPolicy>((x) =>
{
    var policy = Policy.BulkheadAsync(
        maxParallelization: 5,
        maxQueuingActions: 5);

    return policy;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
