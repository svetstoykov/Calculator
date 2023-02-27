using Calculator.Web.Expressions.Models.Request;
using FluentValidation;

namespace Calculator.Web.Expressions.Validators;

public class ExpressionRequestModelValidator : AbstractValidator<ExpressionRequestModel>
{
    public ExpressionRequestModelValidator()
    {
        RuleFor(e => e.Expression)
            .NotNull()
            .NotEmpty();
    }
}