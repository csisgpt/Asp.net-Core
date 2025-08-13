using MediatR;
using InsuranceSuite.Application.Common.Interfaces;

namespace InsuranceSuite.Application.Common.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IUnitOfWork _uow;

    public TransactionBehavior(IUnitOfWork uow)
        => _uow = uow;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();
        await _uow.SaveChangesAsync(cancellationToken);
        return response;
    }
}
