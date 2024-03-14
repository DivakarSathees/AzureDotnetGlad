using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;


var builder = WebApplication.CreateBuilder(args);

// builder.Configuration.AddJsonFile("appsettings.json");

var keyVaultUrl = builder.Configuration["KeyVaultConfiguration:URL"];
var secretkey = builder.Configuration["KeyVaultConfiguration:ConnectionStringSecretName"];
Console.WriteLine(keyVaultUrl);
if (keyVaultUrl != null)
{
    var builtConfig = new ConfigurationBuilder()
        .AddConfiguration(builder.Configuration)
        .Build();

    // builder.Configuration.AddAzureKeyVault(keyVaultUrl);
}
// builder.Configuration.AddAzureKeyVault(keyVaultUrl);




// Retrieve SQL connection string from Key Vault
// var sqlSecretClient = new SecretClient(new Uri("https://newkeyvlt24.vault.azure.net/"), new DefaultAzureCredential());
// var sqlConnectionStringSecret = await sqlSecretClient.GetSecretAsync("blobconnsecret");
// var sqlConnectionString = sqlConnectionStringSecret.Value.Value;
// Console.WriteLine(sqlConnectionString);


var keyVaultConfig = builder.Configuration.GetSection("KeyVaultConfiguration").Get<KeyVaultConfiguration>();

var secretClient = new SecretClient(new Uri(keyVaultConfig.URL), new DefaultAzureCredential());

var connectionStringSecret = secretClient.GetSecret(keyVaultConfig.ConnectionStringSecretName).Value;
var connectionString = connectionStringSecret.Value;
Console.WriteLine(connectionString);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, 
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
