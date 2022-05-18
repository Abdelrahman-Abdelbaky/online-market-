using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers.AdminInterfaces
{
    public interface ICommentData
    {
        public IActionResult ShowComments();
    }
}
