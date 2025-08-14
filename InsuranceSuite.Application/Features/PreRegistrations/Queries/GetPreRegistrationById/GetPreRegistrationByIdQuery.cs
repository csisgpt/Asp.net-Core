using InsuranceSuite.Application.Common.Exceptions;
using InsuranceSuite.Application.Common.Interfaces;
using InsuranceSuite.Application.Features.PreRegistrations.Dto;
using InsuranceSuite.Domain.Entities;
using Mapster;
using MediatR;

namespace InsuranceSuite.Application.Features.PreRegistrations.Queries.GetPreRegistrationById;

public record GetPreRegistrationByIdQuery(Guid Id) : IRequest<PreRegistrationDto>;

public class GetPreRegistrationByIdQueryHandler : IRequestHandler<GetPreRegistrationByIdQuery, PreRegistrationDto>
{
    private readonly IRepository<PreRegistration> _preRepo;

    public GetPreRegistrationByIdQueryHandler(IRepository<PreRegistration> preRepo)
        => _preRepo = preRepo;

    public async Task<PreRegistrationDto> Handle(GetPreRegistrationByIdQuery request, CancellationToken cancellationToken)
    {
        var pre = await _preRepo.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("PreRegistration not found");

        return pre.Adapt<PreRegistrationDto>();
    }
}
