using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers.Interfaces
{
    public interface IcommentModification
    {
        public IActionResult DeleteComment(int id);

    }
}
