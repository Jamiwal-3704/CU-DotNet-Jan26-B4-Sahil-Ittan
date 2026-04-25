using FluentValidation;
using InsureTrust.API.DTOs.Payment;

namespace InsureTrust.API.Validators;

public class InitiatePaymentDtoValidator : AbstractValidator<InitiatePaymentDto>
{
    private static readonly string[] ValidMethods = { "NetBanking", "UPI", "CreditCard", "DebitCard", "Wallet" };

    public InitiatePaymentDtoValidator()
    {
        RuleFor(x => x.UserPolicyId)
            .GreaterThan(0).WithMessage("A valid policy must be selected.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Payment amount must be greater than 0.");

        RuleFor(x => x.PaymentMethod)
            .NotEmpty().WithMessage("Payment method is required.")
            .Must(m => ValidMethods.Contains(m))
            .WithMessage($"Payment method must be one of: {string.Join(", ", ValidMethods)}.");
    }
}
