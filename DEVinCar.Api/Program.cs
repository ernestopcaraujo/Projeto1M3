using System.Text.Json.Serialization;
using DEVinCar.Infra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using DEVinCar.Api.Security;
using System.Text;
using DEVinCar.Domain.Interfaces;
using DEVinCar.Domain.Services;
using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Domain.DTOs;
using Microsoft.AspNetCore.Authentication;
using DEVinCar.Api.Config;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DevInCarDbContext>();
builder.Services.AddScoped<ILoginService,LoginService>();
builder.Services.AddScoped<IDeliveryService,DeliveryService>();
builder.Services.AddScoped<ISalesService,SalesService>();
builder.Services.AddScoped<IUsersService,UsersService>();
builder.Services.AddMemoryCache();

var key = Encoding.ASCII.GetBytes(Settings.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddMvc(config =>
    {
        config.ReturnHttpNotAcceptable = true;
        config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
        config.InputFormatters.Add(new XmlSerializerInputFormatter(config));

    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// comentando para conseguir trabalhar com Insomnia/Postman via http comum
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ErrorMiddleware>();

app.Run();
