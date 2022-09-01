using Eskisehirspor.Application;
using Eskisehirspor.Infrastructure;
using Eskisehirspor.Infrastructure.Authentication.Jwt;
using Eskisehirspor.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);
AddJwtService(builder.Services, builder.Configuration);
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

void AddJwtService(IServiceCollection services, IConfiguration configuration)
{
    var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
          options.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateAudience = true,
              ValidateIssuer = true,
              ValidateLifetime = true,
              ValidateIssuerSigningKey = true,
              ValidAudience = tokenOptions.Audience,
              ValidIssuer = tokenOptions.Issuer,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
              LifetimeValidator = (notBefore, expires, tokenToValidate, tokenValidationParameters) => expires != null && expires > DateTime.UtcNow
          };
      });
}