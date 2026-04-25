using FluentValidation;
using InsureTrust.API.DTOs.Policy;

namespace InsureTrust.API.Validators;

public class CreatePolicyTypeDtoValidator : AbstractValidator<CreatePolicyTypeDto>
{
    public CreatePolicyTypeDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Policy type name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .Must(c => c == "Personal" || c == "Business")
            .WithMessage("Category must be 'Personal' or 'Business'.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500);

        RuleFor(x => x.BaseMonthlyPremium)
            .GreaterThan(0).WithMessage("Base monthly premium must be greater than 0.");

        RuleFor(x => x.MinTenureMonths)
            .GreaterThanOrEqualTo(1).WithMessage("Minimum tenure must be at least 1 month.");

        RuleFor(x => x.MaxTenureMonths)
            .GreaterThan(x => x.MinTenureMonths)
            .WithMessage("Maximum tenure must be greater than minimum tenure.");
    }
}

public class CreatePolicyDtoValidator : AbstractValidator<CreatePolicyDto>
{
    private static readonly decimal[] AllowedAmounts = { 5000m, 10000m, 15000m };

    public CreatePolicyDtoValidator()
    {
        RuleFor(x => x.PolicyTypeId)
            .GreaterThan(0).WithMessage("A valid policy type must be selected.");

        RuleFor(x => x.Tenure)
            .GreaterThan(0).WithMessage("Tenure must be greater than 0 months.");

        RuleFor(x => x.PackageAmount)
            .Must(a => AllowedAmounts.Contains(a))
            .WithMessage("Package amount must be ₹5,000, ₹10,000, or ₹15,000.");
    }
}

public class ApprovePolicyDtoValidator : AbstractValidator<ApprovePolicyDto>
{
    public ApprovePolicyDtoValidator()
    {
        RuleFor(x => x.Action)
            .NotEmpty().WithMessage("Action is required.")
            .Must(a => a == "Grant" || a == "Reject")
            .WithMessage("Action must be 'Grant' or 'Reject'.");

        When(x => x.Action == "Reject", () =>
        {
            RuleFor(x => x.AdminRemarks)
                .NotEmpty().WithMessage("Admin remarks are required when rejecting a policy.");
        });
    }
}

public class EditPolicyDtoValidator : AbstractValidator<EditPolicyDto>
{
    public EditPolicyDtoValidator()
    {
        RuleFor(x => x.Tenure)
            .GreaterThanOrEqualTo(0).WithMessage("Tenure cannot be negative.");

        RuleFor(x => x.PackageAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Package amount cannot be negative.");
    }
}
