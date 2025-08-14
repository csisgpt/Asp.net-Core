using InsuranceSuite.Application.Common.Interfaces;
using InsuranceSuite.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InsuranceSuite.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly InsuranceDbContext _context;
    private readonly DbSet<T> _set;

    public Repository(InsuranceDbContext context)
    {
        _context = context;
        _set = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _set.FindAsync(new object?[] { id }, cancellationToken);

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
        => await _set.AddAsync(entity, cancellationToken);
}
