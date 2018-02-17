using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Synetec.Hr.Web.Controllers
{
    [Authorize]
    public class HolidaysController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}