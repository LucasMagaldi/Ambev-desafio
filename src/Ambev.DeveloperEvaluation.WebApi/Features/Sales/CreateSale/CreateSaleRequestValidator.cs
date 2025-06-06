using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validates CreateSaleRequest data
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.Customer).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Branch).NotEmpty();
        RuleForEach(x => x.Items).SetValidator(new CreateSaleItemRequestValidator());
    }
}

public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
{
    public CreateSaleItemRequestValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.ProductName).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Discount).GreaterThanOrEqualTo(0);
    }
}
