using System.Text;
using System.Text.Json.Serialization;
using EthioClass.Application.Common.Behaviors;
using EthioClass.Application.Common.Interfaces;
using EthioClass.Infrastructure;
using EthioClass.Infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(cfg =>cfg.RegisterServicesFromAssembly(typeof(EthioClass.Application.Schools.Commands
    .CreateSchoolCommand).Assembly));
    builder.Services.AddValidatorsFromAssembly(typeof(EthioClass.Application.Schools.Commands.CreateSchoolCommand).Assembly);
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var jwtSettings = builder.Configuration.GetSection("jwtSettings").Get<JwtSettings>()!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
    };
});
    builder.Services.AddAuthorization();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<ICurrentUserService, EthioClass.Api.Services.CurrentUserService>();
    var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();