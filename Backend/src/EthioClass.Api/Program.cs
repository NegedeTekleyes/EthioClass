using EthioClass.Infrastructure;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(cfg =>cfg.RegisterServicesFromAssembly(typeof(EthioClass.Application.Schools.Commands
    .CreateSchoolCommand).Assembly));
    builder.Services.AddValidatorsFromAssembly(typeof(EthioClass.Application.Schools.Commands.CreateSchoolCommand).Assembly);

    var app = builder.Build();
    app.MapControllers();
    app.Run();