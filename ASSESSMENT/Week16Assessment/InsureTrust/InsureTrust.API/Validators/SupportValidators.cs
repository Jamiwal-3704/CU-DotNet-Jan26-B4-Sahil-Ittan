using FluentValidation;
using InsureTrust.API.DTOs.Support;

namespace InsureTrust.API.Validators;

public class CreateSupportQueryDtoValidator : AbstractValidator<CreateSupportQueryDto>
{
    public CreateSupportQueryDtoValidator()
    {
        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Subject is required.")
            .MaximumLength(200).WithMessage("Subject must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MinimumLength(10).WithMessage("Description must be at least 10 characters.")
            .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.");
    }
}

public class UpdateSupportStatusDtoValidator : AbstractValidator<UpdateSupportStatusDto>
{
    private static readonly string[] ValidStatuses = { "Pending", "In Progress", "Resolved", "Closed" };

    public UpdateSupportStatusDtoValidator()
    {
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .Must(s => ValidStatuses.Contains(s))
            .WithMessage($"Status must be one of: {string.Join(", ", ValidStatuses)}.");
    }
}
