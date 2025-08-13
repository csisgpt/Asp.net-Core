using FluentValidation;

namespace InsuranceSuite.Application.Features.PreRegistrations.Commands.CreatePreRegistration;

public class CreatePreRegistrationCommandValidator : AbstractValidator<CreatePreRegistrationCommand>
{
    public CreatePreRegistrationCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ContractId).NotEmpty();
        RuleFor(x => x.SubContractId).NotEmpty();
        RuleFor(x => x.Category).NotEmpty().MaximumLength(200);
    }
}
