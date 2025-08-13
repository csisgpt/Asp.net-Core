using InsuranceSuite.Application.Common.Interfaces;
using InsuranceSuite.Infrastructure.Persistence;

namespace InsuranceSuite.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly InsuranceDbContext _context;

    public UnitOfWork(InsuranceDbContext context)
        => _context = context;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        => _context.SaveChangesAsync(cancellationToken);
}
