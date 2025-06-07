using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleValidator()
    {
        RuleFor(x => x.Customer).NotEmpty();
        RuleFor(x => x.Branch).NotEmpty();
        RuleFor(x => x.Date).NotEmpty();
        RuleForEach(x => x.Items).SetValidator(new UpdateSaleItemValidator());
    }

    private class UpdateSaleItemValidator : AbstractValidator<UpdateSaleItemDto>
    {
        public UpdateSaleItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.Quantity).InclusiveBetween(1, 20);
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
