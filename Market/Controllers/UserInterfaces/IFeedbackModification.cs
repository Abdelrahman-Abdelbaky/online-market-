using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers.UserInterfaces
{
    public interface IFeedbackModification
    {
        public IActionResult Feedback();
        public IActionResult AddFeedback(Feedback feedback);
    }
}
