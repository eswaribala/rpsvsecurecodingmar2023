using BankingApp.Configurations;
using BankingApp.Contexts;
using BankingApp.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using reCAPTCHA.AspNetCore;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Microsoft.AspNetCore.Identity;
using BankingApp.Areas.Identity.Data;
using BankingApp.Areas.Identity;
using BankingApp.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("BankingAppIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'BankingAppIdentityDbContextConnection' not found.");
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

SqlConnectionStringBuilder providerIdentityCs = new SqlConnectionStringBuilder();
//reading from Vault server
providerIdentityCs.InitialCatalog = data["vsecureidentitydbname"].ToString();
providerIdentityCs.UserID = data["username"].ToString();
providerIdentityCs.Password = data["password"].ToString();
//providerCs.DataSource = "DESKTOP-55AGI0I\\MSSQLEXPRESS2022";
//reading via config server
providerIdentityCs.DataSource = configuration["trainerservername"];

//providerCs.UserID = CryptoService2.Decrypt(ConfigurationManager.
//AppSettings["UserId"]);
providerIdentityCs.MultipleActiveResultSets = true;
providerIdentityCs.TrustServerCertificate = false;


//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
// .AddEntityFrameworkStores<BankingAppIdentityDbContext>();

builder.Services.AddDbContext<BankingAppIdentityDbContext>(o =>
o.UseSqlServer(providerIdentityCs.ToString()));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<BankingAppIdentityDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;   
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IEmailReputation, EmailReputation>();
builder.Services.AddSingleton<IPasswordHasher<Customer>, PasswordHasher>();
builder.Services.AddScoped<IPasswordHasher<IdentityUser>, BCryptPasswordHasher<IdentityUser>>();
builder.Services.AddScoped<ICryptoService,CryptoService>();
builder.Services.AddDataProtection();
builder.Services.AddRecaptcha(configuration.GetSection("RecaptchaSettings"));
builder.Services.AddMvc(options =>
{
    options.CacheProfiles.Add("NoCache",
        new CacheProfile()
        {
            Duration = 30,
            Location = ResponseCacheLocation.None,
            NoStore = true
        });
});

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
