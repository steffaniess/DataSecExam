using Microsoft.AspNetCore.Mvc;

namespace Server_API.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
