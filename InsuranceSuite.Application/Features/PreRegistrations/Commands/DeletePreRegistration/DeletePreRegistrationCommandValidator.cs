using FluentValidation;

namespace InsuranceSuite.Application.Features.PreRegistrations.Commands.DeletePreRegistration;

public class DeletePreRegistrationCommandValidator : AbstractValidator<DeletePreRegistrationCommand>
{
    public DeletePreRegistrationCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
