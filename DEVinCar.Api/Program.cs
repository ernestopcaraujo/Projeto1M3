using System.Text.Json.Serialization;
using DEVinCar.Infra.Data;
using DEVinCar.Infra.Data.Repositories;
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
using DEVinCar.Domain.Interfaces.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                                              Escreva 'Bearer' [espaço] e o token gerado no login na caixa abaixo.
                                              Exemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
     c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                                          {
                                            {
                                              new OpenApiSecurityScheme
                                              {
                                                Reference = new OpenApiReference
                                                  {
                                                    Type = ReferenceType.SecurityScheme,
                                                    Id = JwtBearerDefaults.AuthenticationScheme
                                                  },
                                                },
                                                new List<string>()
                                              }
                                            });
});
builder.Services.AddDbContext<DevInCarDbContext>();

builder.Services.AddScoped<ILoginService,LoginService>();
builder.Services.AddScoped<ILoginRepository,LoginRepository>();

builder.Services.AddScoped<ICarsService,CarsService>();
builder.Services.AddScoped<ICarsRepository,CarsRepository>();

builder.Services.AddScoped<IUsersService,UsersService>();
builder.Services.AddScoped<IUsersRepository,UsersRepository>();

builder.Services.AddScoped<IDeliveryService,DeliveryService>();
builder.Services.AddScoped<IDeliveryRepository,DeliveryRepository>();

builder.Services.AddScoped<ISalesService,SalesService>();
builder.Services.AddScoped<ISalesRepository,SalesRepository>();

builder.Services.AddMemoryCache();
builder.Services.AddScoped(typeof(CacheService<>));

var key = Encoding.ASCII.GetBytes(Settings.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
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
builder.Services.AddAuthorization();

builder.Services.AddMvc(config =>
    {
        config.ReturnHttpNotAcceptable = true;
        config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
        config.InputFormatters.Add(new XmlSerializerInputFormatter(config));
        // config.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
        // config.InputFormatters.Add(new XmlDataContractSerializerInputFormatter(config));

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// comentando para conseguir trabalhar com Insomnia/Postman via http comum
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//app.UseMiddleware<ErrorMiddleware>();

app.Run();
