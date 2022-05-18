using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers.UserInterfaces
{
    public interface ICommentModification
    {
        public Task<IActionResult> Comments(product product, int id);
        public IActionResult AddComments(Comments comment);

    }
}
