using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers.Interfaces
{
    public interface IFeedbackModification
    {
        public IActionResult DeleteFeedback(int id);
    }
}
