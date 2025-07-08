namespace Store.App.GraphQl.Validation;

public interface IEmailValidator
{
    bool ValidateEmail(string email);
}