using Market.Models;

namespace Market.Controllers.UserInterfaces
{
    public interface IUserControl
    {
        public Boolean CheckUserEmail(User model);
        public Boolean CheckUserPassword(User model);

    }
}
