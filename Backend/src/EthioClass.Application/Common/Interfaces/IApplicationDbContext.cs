using Microsoft.EntityFrameworkCore;
using EthioClass.Domain.Entities;

namespace EthioClass.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<School> Schools { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}