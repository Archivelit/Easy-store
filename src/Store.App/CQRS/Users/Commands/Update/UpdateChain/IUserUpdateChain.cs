using Store.Core.Builders;
using Store.Core.Models.Dto.User;

namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

public interface IUserUpdateChain
{
    IUserUpdateChain SetNext(IUserUpdateChain next);
    UserBuilder Update(UserBuilder builder, UserDto model);
}