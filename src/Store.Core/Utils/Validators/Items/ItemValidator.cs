using FluentValidation;
using Store.Core.Models;

namespace Store.Core.Utils.Validators.Items;

public class ItemValidator : AbstractValidator<Item>
{
    public ItemValidator()
    {
        RuleSet("Title", () =>
        {
            RuleFor(i => i.Title).SetValidator(new TitleValidator());
        });

        RuleSet("Description", () =>
        {
            RuleFor(i => i.Description).SetValidator(new DescriptionValidator());
        });

        RuleSet("Price", () =>
        {
            RuleFor(i => i.Price).SetValidator(new PriceValidator());
        });

        RuleSet("Quantity", () =>
        {
            RuleFor(i => i.QuantityInStock).SetValidator(new QuantityValidator());
        });

        RuleSet("UserId", () =>
        {
            RuleFor(i => i.UserId).SetValidator(new UserIdValidator());
        });

        RuleSet("Id", () =>
        {
            RuleFor(i => i.Id).SetValidator(new ItemIdValidator());
        });

        RuleSet("CreatedAt", () =>
        {
            RuleFor(i => i).SetValidator(new CreatedAtValidator());
        });

        RuleSet("UpdatedAt", () =>
        {
            RuleFor(i => i).SetValidator(new UpdatedAtValidator());
        });
    }
}

public class ItemIdValidator : AbstractValidator<Guid>
{
    public ItemIdValidator()
    {
        RuleFor(x => x).NotEmpty();
    }
}

public class UserIdValidator : AbstractValidator<Guid>
{
    public UserIdValidator()
    {
        RuleFor(x => x).NotEmpty();
    }
}

public class TitleValidator : AbstractValidator<string>
{
    public TitleValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Title cannot be empty");

        RuleFor(x => x)
            .Length(3, 100)
            .WithMessage("Title must be between 3 and 100 chars");

        RuleFor(x => x)
            .Must(x => x.Any(char.IsLetter))
            .WithMessage("Title must contain atleast 1 letter");
    }
}

public class DescriptionValidator : AbstractValidator<string?>
{
    public DescriptionValidator()
    {
        RuleFor(x => x)
            .Length(1, 2000)
            .When(x => x != null)
            .WithMessage("Description cannot be longer 2000 characters");

        RuleFor(x => x)
            .Must(x => x.Any(char.IsLetter))
            .When(x => x != null)
            .WithMessage("Title must contain atleast 1 letter");
    }
}

public class PriceValidator : AbstractValidator<decimal>
{
    public PriceValidator()
    {
        RuleFor(x => x)
            .GreaterThan(0)
            .WithMessage("Price cannot be under 0 inclusive");
    }
}

public class QuantityValidator : AbstractValidator<int>
{
    public QuantityValidator()
    {
        RuleFor(x => x)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Quantity cannot be under 0");
    }
}

public class CreatedAtValidator : AbstractValidator<Item>
{
    public CreatedAtValidator()
    {
        RuleFor(x => x);
    }
}

public class UpdatedAtValidator : AbstractValidator<Item>
{
    public UpdatedAtValidator()
    {
        RuleFor(x => x);
    }
}