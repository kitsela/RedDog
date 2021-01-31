using FluentValidation;
using RedDog.API.Handlers.User.LoginHandler;

public class LoginQueryValidation : AbstractValidator<LoginQuery>
{
	public LoginQueryValidation()
	{
		RuleFor(x => x.Email).EmailAddress().NotEmpty();
		RuleFor(x => x.Password).NotEmpty();
	}
}