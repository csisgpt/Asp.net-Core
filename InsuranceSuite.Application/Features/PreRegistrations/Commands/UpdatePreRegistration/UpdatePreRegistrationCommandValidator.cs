using FluentValidation;

namespace InsuranceSuite.Application.Features.PreRegistrations.Commands.UpdatePreRegistration;

public class UpdatePreRegistrationCommandValidator : AbstractValidator<UpdatePreRegistrationCommand>
{
    public UpdatePreRegistrationCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.ContractId).NotEmpty();
        RuleFor(x => x.SubContractId).NotEmpty();
        RuleFor(x => x.Category).NotEmpty().MaximumLength(200);
    }
}
