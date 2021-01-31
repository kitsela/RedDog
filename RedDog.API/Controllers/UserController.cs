using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedDog.API.Handlers.User.LoginHandler;
using RedDog.Common.Models.User;
using System.Threading.Tasks;

namespace RedDog.API.Controllers
{
    public class UserController : BaseController
    {
        #region Auth

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> LoginAsync(LoginQuery query)
        {
            return await Mediator.Send(query);
        }


        #endregion

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
