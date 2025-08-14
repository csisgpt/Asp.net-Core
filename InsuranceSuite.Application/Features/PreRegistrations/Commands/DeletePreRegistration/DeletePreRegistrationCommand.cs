using InsuranceSuite.Application.Common.Exceptions;
using InsuranceSuite.Application.Common.Interfaces;
using InsuranceSuite.Domain.Entities;
using MediatR;

namespace InsuranceSuite.Application.Features.PreRegistrations.Commands.DeletePreRegistration;

public record DeletePreRegistrationCommand(Guid Id) : IRequest;

public class DeletePreRegistrationCommandHandler : IRequestHandler<DeletePreRegistrationCommand>
{
    private readonly IRepository<PreRegistration> _preRepo;

    public DeletePreRegistrationCommandHandler(IRepository<PreRegistration> preRepo)
        => _preRepo = preRepo;

    public async Task<Unit> Handle(DeletePreRegistrationCommand request, CancellationToken cancellationToken)
    {
        var pre = await _preRepo.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("PreRegistration not found");

        await _preRepo.DeleteAsync(pre, cancellationToken);
        return Unit.Value;
    }
}
