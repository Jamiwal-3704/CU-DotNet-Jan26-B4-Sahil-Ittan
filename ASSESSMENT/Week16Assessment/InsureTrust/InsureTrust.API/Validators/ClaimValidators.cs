using FluentValidation;
using InsureTrust.API.DTOs.Claim;

namespace InsureTrust.API.Validators;

public class SubmitClaimDtoValidator : AbstractValidator<SubmitClaimDto>
{
    public SubmitClaimDtoValidator()
    {
        RuleFor(x => x.Documents)
            .NotNull().WithMessage("At least one document is required.")
            .Must(d => d != null && d.Count > 0).WithMessage("At least one document must be attached.");
    }
}

public class UpdateClaimDtoValidator : AbstractValidator<UpdateClaimDto>
{
    public UpdateClaimDtoValidator()
    {
        RuleFor(x => x.Action)
            .NotEmpty().WithMessage("Action is required.")
            .Must(a => a == "Approve" || a == "Deny")
            .WithMessage("Action must be 'Approve' or 'Deny'.");

        When(x => x.Action == "Approve", () =>
        {
            RuleFor(x => x.MaturityAmount)
                .GreaterThan(0).WithMessage("Maturity amount must be greater than 0 when approving.");
        });

        When(x => x.Action == "Deny", () =>
        {
            RuleFor(x => x.AdminRemarks)
                .NotEmpty().WithMessage("Admin remarks are required when denying a claim.");
        });
    }
}
