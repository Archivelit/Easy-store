namespace API.Requests;

public record AuthenticateCustomerRequest(string Email, string Password);