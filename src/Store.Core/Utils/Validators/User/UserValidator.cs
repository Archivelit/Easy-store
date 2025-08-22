using FluentValidation;
using System.Net.Mail;

namespace Store.Core.Utils.Validators.User;

public class UserValidator : AbstractValidator<Models.User>
{
    public UserValidator()
    {
        RuleSet("Name", () =>
        {
            RuleFor(u => u.Name).SetValidator(new NameValidator());
        });

        RuleSet("Email", () =>
        {
            RuleFor(u => u.Email).SetValidator(new EmailValidator());
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
            .Must(BeValidEmail)
            .When(e => !string.IsNullOrEmpty(e))
            .WithMessage("Invalid email address format");
    }

    private bool BeValidEmail(string email)
    {
        return MailAddress.TryCreate(email, out var addr) && addr.Address == email;
    }
}