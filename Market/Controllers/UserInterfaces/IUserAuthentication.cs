using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers.UserInterfaces
{
    public interface IUserAuthentication
    {
        public IActionResult UserRegister(User user);
        public IActionResult UserLogin(User model);
        public IActionResult Register();

    }
}
