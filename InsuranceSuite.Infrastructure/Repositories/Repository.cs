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

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        => await _set.ToListAsync(cancellationToken);

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
        => await _set.AddAsync(entity, cancellationToken);

    public Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _set.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        _set.Remove(entity);
        return Task.CompletedTask;
    }
}
