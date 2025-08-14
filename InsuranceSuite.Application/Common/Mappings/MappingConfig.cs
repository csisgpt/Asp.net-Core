using InsuranceSuite.Application.Features.PreRegistrations.Dto;
using InsuranceSuite.Domain.Entities;
using Mapster;

namespace InsuranceSuite.Application.Common.Mappings;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PreRegistration, PreRegistrationDto>();
    }
}
