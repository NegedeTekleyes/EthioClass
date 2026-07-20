using EthioClass.Application.Common.Behaviors;
using EthioClass.Infrastructure;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(cfg =>cfg.RegisterServicesFromAssembly(typeof(EthioClass.Application.Schools.Commands
    .CreateSchoolCommand).Assembly));
    builder.Services.AddValidatorsFromAssembly(typeof(EthioClass.Application.Schools.Commands.CreateSchoolCommand).Assembly);
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    var app = builder.Build();
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Run();