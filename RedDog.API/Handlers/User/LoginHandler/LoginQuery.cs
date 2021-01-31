using MediatR;
using RedDog.Common.Models.User;

namespace RedDog.API.Handlers.User.LoginHandler
{
    public class LoginQuery : IRequest<UserModel>
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
