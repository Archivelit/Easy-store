namespace Store.Core.Utils.Validators;

public class ItemValidator : AbstractValidator<Item>
{
    private readonly IValidator<string> _titleValidator;
    private readonly IValidator<string?> _descriptionValidator;
    private readonly IValidator<decimal> _priceValidator;
    private readonly IValidator<int> _quantityValidator;
    private readonly IValidator<Guid> _itemIdValidator;
    private readonly IValidator<Guid> _userIdValidator;
    private readonly IValidator<DateTime> _createdAtValidator;
    private readonly IValidator<Item> _updatedAtValidator;

    public ItemValidator([FromKeyedServices(KeyedServicesKeys.ItemTitleValidator)] IValidator<string> titleValidator, 
        [FromKeyedServices(KeyedServicesKeys.ItemDescriptionValidator)]IValidator<string?> descriptionValidator, 
        [FromKeyedServices(KeyedServicesKeys.ItemPriceValidator)] IValidator<decimal> priceValidator, 
        [FromKeyedServices(KeyedServicesKeys.ItemQuantityValidator)] IValidator<int> quantityValidator,
        [FromKeyedServices(KeyedServicesKeys.ItemIdValidator)] IValidator<Guid> itemIdValidator,
        [FromKeyedServices(KeyedServicesKeys.UserIdValiadator)] IValidator<Guid> userIdValidator,
        [FromKeyedServices(KeyedServicesKeys.CreatedAtValidator)] IValidator<DateTime> createdAtValidator,
        [FromKeyedServices(KeyedServicesKeys.UpdatedAtValidator)] IValidator<Item> updatedAtValidator)
    {
        _titleValidator = titleValidator;
        _descriptionValidator = descriptionValidator;
        _priceValidator = priceValidator;
        _quantityValidator = quantityValidator;
        _itemIdValidator = itemIdValidator;
        _userIdValidator = userIdValidator;
        _createdAtValidator = createdAtValidator;
        _updatedAtValidator = updatedAtValidator;

        RuleSet("Title", () =>
        {
            RuleFor(i => i.Title).SetValidator(_titleValidator);
        });

        RuleSet("Description", () =>
        {
            RuleFor(i => i.Description).SetValidator(_descriptionValidator);
        });

        RuleSet("Price", () =>
        {
            RuleFor(i => i.Price).SetValidator(_priceValidator);
        });

        RuleSet("Quantity", () =>
        {
            RuleFor(i => i.QuantityInStock).SetValidator(_quantityValidator);
        });

        RuleSet("UserId", () =>
        {
            RuleFor(i => i.UserId).SetValidator(_userIdValidator);
        });

        RuleSet("Id", () =>
        {
            RuleFor(i => i.Id).SetValidator(_itemIdValidator);
        });

        RuleSet("CreatedAt", () =>
        {
            RuleFor(i => i.CreatedAt).SetValidator(_createdAtValidator);
        });

        RuleSet("UpdatedAt", () =>
        {
            RuleFor(i => i).SetValidator(_updatedAtValidator);
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

public class ItemTitleValidator : AbstractValidator<string>
{
    public ItemTitleValidator()
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

public class ItemDescriptionValidator : AbstractValidator<string?>
{
    public ItemDescriptionValidator()
    {
        RuleFor(x => x)
            .Length(1, 2000)
            .When(x => x != null)
            .WithMessage("Description cannot be longer 2000 characters");

        RuleFor(x => x)
            .Must(x => x!.Any(char.IsLetter))
            .When(x => x != null)
            .WithMessage("Title must contain atleast 1 letter");
    }
}

public class ItemPriceValidator : AbstractValidator<decimal>
{
    public ItemPriceValidator()
    {
        RuleFor(x => x)
            .GreaterThan(0)
            .WithMessage("Price cannot be under 0 inclusive");
    }
}

public class ItemQuantityValidator : AbstractValidator<int>
{
    public ItemQuantityValidator()
    {
        RuleFor(x => x)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Quantity cannot be under 0");
    }
}

public class CreatedAtValidator : AbstractValidator<DateTime>
{
    public CreatedAtValidator()
    {
        RuleFor(d => d)
            .LessThan(d => DateTime.Now)
            .GreaterThan(d => DateTime.MinValue);
    }
}

public class UpdatedAtValidator : AbstractValidator<Item>
{
    public UpdatedAtValidator()
    {
        RuleFor(d => d.UpdatedAt)
            .LessThanOrEqualTo(d => DateTime.Now)
            .GreaterThan(d => d.CreatedAt)
            .When(d => d != null);
    }
}