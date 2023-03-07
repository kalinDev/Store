using FluentValidation;
using Store.Catalog.Domain.Entities;

namespace Store.Catalog.Application.Validations;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.Name).Length(2, 250).NotEmpty();
        RuleFor(x => x.Description).Length(2, 250);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.ImageUrl).Length(2, 250).NotEmpty();
        RuleFor(x => x.RegistredAt).GreaterThan(DateTime.Now.AddDays(-1));
        RuleFor(x => x.StockQuantity).GreaterThanOrEqualTo(0);
    }
}