using Microsoft.AspNetCore.Mvc;

namespace API.Requests;
public record RegisterCustomerRequest(string Name, string Email, string Password);