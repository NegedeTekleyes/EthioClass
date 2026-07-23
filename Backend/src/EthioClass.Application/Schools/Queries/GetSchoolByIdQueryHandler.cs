using MediatR;
using Microsoft.EntityFrameworkCore;
using EthioClass.Application.Common.Interfaces;
using EthioClass.Application.Schools.Dtos;

namespace EthioClass.Application.Schools.Queries;

public class GetSchoolByIdQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    : IRequestHandler<GetSchoolByIdQuery, SchoolResponseDto?>
{
    public async Task<SchoolResponseDto?> Handle(GetSchoolByIdQuery request, CancellationToken cancellationToken)
    {
        if (currentUser.SchoolId != request.Id)
            return null;

        return await context.Schools
            .AsNoTracking()
            .Where(s => s.Id == request.Id)
            .Select(s => new SchoolResponseDto(
                s.Id, s.Name, s.NameAmharic, s.Code,
                s.Email, s.PhoneNumber, s.Address, s.City, s.Region,
                s.IsActive))
            .FirstOrDefaultAsync(cancellationToken);
    }
}