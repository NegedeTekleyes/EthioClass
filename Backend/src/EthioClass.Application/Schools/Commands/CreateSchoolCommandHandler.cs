using EthioClass.Application.Common.Interfaces;
using EthioClass.Application.Schools.Dtos;
using EthioClass.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EthioClass.Application.Schools.Commands;

public class CreateSchoolCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateSchoolCommand, SchoolResponseDto>
{
    public async Task<SchoolResponseDto> Handle(CreateSchoolCommand request, CancellationToken ct)
    {
        
     var codeExists = await context.Schools.AnyAsync(s => s.Code == request.Code, ct);
     if (codeExists)
         throw new InvalidOperationException($"School code '{request.Code}' already exists.");

     var school = new School()
     {
         Name = request.Name,
         NameAmharic = request.NameAmharic,
         Code = request.Code,
         Address = request.Address,
         City = request.City,
         PhoneNumber = request.PhoneNumber
     };
     context.Schools.Add(school);
     await context.SaveChangesAsync(ct);

     return new SchoolResponseDto(school.Id,school.Name, school.NameAmharic, school.Code, school.IsActive);

    }
}