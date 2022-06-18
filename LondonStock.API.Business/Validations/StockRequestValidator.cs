using FluentValidation;
using FluentValidation.Results;
using LondonStock.API.Common.Models;

namespace LondonStock.API.Business.Validations
{
    public class StockRequestValidator : AbstractValidator<StockRequest>
    {
        public StockRequestValidator()
        {
            RuleFor(x => x.BrockerId).NotEmpty();
            RuleFor(x => x.TotalShare).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
        }

        public override ValidationResult Validate(ValidationContext<StockRequest> context)
        {
            return context.InstanceToValidate == null
                ? new ValidationResult(new[] { new ValidationFailure(nameof(StockRequest), $"{nameof(StockRequest)} cannot be null or empty") })
                : base.Validate(context);
        }
    }
}
