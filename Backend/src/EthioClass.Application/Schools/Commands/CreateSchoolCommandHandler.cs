using MediatR;
using Microsoft.EntityFrameworkCore;
using EthioClass.Application.Common.Interfaces;
using EthioClass.Application.Schools.Dtos;
using EthioClass.Domain.Entities;

namespace EthioClass.Application.Schools.Commands;

public class CreateSchoolCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateSchoolCommand, SchoolResponseDto>
{
    public async Task<SchoolResponseDto> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
    {
        var codeExists = await context.Schools.AnyAsync(s => s.Code == request.Code, cancellationToken);
        if (codeExists)
            throw new InvalidOperationException($"School code '{request.Code}' already exists.");

        var school = new School
        {
            Name = request.Name,
            NameAmharic = request.NameAmharic,
            Code = request.Code,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            City = request.City,
            Region = request.Region,
            Website = request.Website,
            Motto = request.Motto
        
        };

        context.Schools.Add(school);
        await context.SaveChangesAsync(cancellationToken);

        return new SchoolResponseDto(
            school.Id, school.Name, school.NameAmharic, school.Code,
            school.Email, school.PhoneNumber, school.Address, school.City, school.Region,
            school.IsActive);
    }
}