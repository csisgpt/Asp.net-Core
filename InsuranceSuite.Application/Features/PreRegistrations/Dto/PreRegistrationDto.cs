using InsuranceSuite.Domain.Enums;

namespace InsuranceSuite.Application.Features.PreRegistrations.Dto;

public record PreRegistrationDto(Guid Id, Guid UserId, Guid ContractId, Guid SubContractId, Guid? HealthPlanId, string Category, RegistrationStatus Status);
