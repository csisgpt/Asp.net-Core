using InsuranceSuite.Application.Common.Interfaces;
using InsuranceSuite.Application.Features.PreRegistrations.Dto;
using InsuranceSuite.Domain.Entities;
using Mapster;
using MediatR;

namespace InsuranceSuite.Application.Features.PreRegistrations.Queries.GetPreRegistrations;

public record GetPreRegistrationsQuery() : IRequest<List<PreRegistrationDto>>;

public class GetPreRegistrationsQueryHandler : IRequestHandler<GetPreRegistrationsQuery, List<PreRegistrationDto>>
{
    private readonly IRepository<PreRegistration> _preRepo;

    public GetPreRegistrationsQueryHandler(IRepository<PreRegistration> preRepo)
        => _preRepo = preRepo;

    public async Task<List<PreRegistrationDto>> Handle(GetPreRegistrationsQuery request, CancellationToken cancellationToken)
    {
        var pres = await _preRepo.GetAllAsync(cancellationToken);
        return pres.Adapt<List<PreRegistrationDto>>();
    }
}
