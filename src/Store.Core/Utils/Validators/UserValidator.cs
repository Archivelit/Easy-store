namespace Store.Core.Utils.Validators.User;

public class UserValidator : AbstractValidator<Models.User>
{
    private readonly EmailValidator _emailValidator;
    private readonly NameValidator _nameValidator;

    public UserValidator(
        [FromKeyedServices(KeyedServicesKeys.EmailValidator)] IValidator<string> emailValidator,
        [FromKeyedServices(KeyedServicesKeys.NameValidator)] IValidator<string> nameValidator)
    {
        _emailValidator = emailValidator as EmailValidator 
            ?? throw new ArgumentException("Invalid email validator provided", nameof(emailValidator));

        _nameValidator = nameValidator as NameValidator
            ?? throw new ArgumentException("Invalid email validator provided", nameof(emailValidator));

        RuleSet("Name", () =>
        {
            RuleFor(u => u.Name).SetValidator(_nameValidator);
        });

        RuleSet("Email", () =>
        {
            RuleFor(u => u.Email).SetValidator(_emailValidator);
        });
    }
}

public class NameValidator : AbstractValidator<string>
{
    public NameValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("User name cannot be empty");

        RuleFor(x => x)
            .MinimumLength(3)
            .WithMessage("Name is too short. It must have atleast 3 characters");

        RuleFor(x => x)
            .MaximumLength(100)
            .WithMessage("Name is too long. It has to be 100 characters long");

        RuleFor(x => x)
            .Must(x => x.Any(char.IsLetter))
            .WithMessage("Name must have atleast 1 letter");
    }
}

public class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Email is required");

        RuleFor(x => x)
            .EmailAddress()
            .When(e => !string.IsNullOrEmpty(e))
            .WithMessage("Invalid email address format");
    }
}

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Password is required");
        RuleFor(x => x)
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long");
        RuleFor(x => x)
            .Must(x => x.Any(char.IsUpper))
            .WithMessage("Password must contain at least one uppercase letter");
        RuleFor(x => x)
            .Must(x => x.Any(char.IsLower))
            .WithMessage("Password must contain at least one lowercase letter");
        RuleFor(x => x)
            .Must(x => x.Any(char.IsDigit))
            .WithMessage("Password must contain at least one digit");
        RuleFor(x => x)
            .Must(x => x.Any(char.IsLetterOrDigit))
            .WithMessage("Password must contain at least one special character");
    }
}