using BankingApp.Configurations;
using BankingApp.Contexts;
using BankingApp.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using reCAPTCHA.AspNetCore;
using Steeltoe.Extensions.Configuration.ConfigServer;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddConfigServer();
ConfigurationManager configuration = builder.Configuration;

Dictionary<String, Object> data = new VaultConfiguration(configuration)
    .GetDBCredentials().Result;
Console.WriteLine(data);
SqlConnectionStringBuilder providerCs = new SqlConnectionStringBuilder();
//reading from Vault server
providerCs.InitialCatalog = data["vsecuredbname"].ToString();
providerCs.UserID = data["username"].ToString();
providerCs.Password = data["password"].ToString();
//providerCs.DataSource = "DESKTOP-55AGI0I\\MSSQLEXPRESS2022";
//reading via config server
providerCs.DataSource = configuration["trainerservername"];

//providerCs.UserID = CryptoService2.Decrypt(ConfigurationManager.
//AppSettings["UserId"]);
providerCs.MultipleActiveResultSets = true;
providerCs.TrustServerCertificate = false;

builder.Services.AddDbContext<CustomerContext>(o =>
o.UseSqlServer(providerCs.ToString()));
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IEmailReputation, EmailReputation>();
builder.Services.AddRecaptcha(configuration.GetSection("RecaptchaSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
