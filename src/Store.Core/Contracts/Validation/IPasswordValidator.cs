namespace Store.App.GraphQl.Validation;

public interface IPasswordValidator
{
    bool ValidatePassword(string password);
}