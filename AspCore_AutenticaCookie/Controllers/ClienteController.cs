using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspCore_AutenticaCookie.Controllers
{

    [Authorize]
    public class ClienteController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

    }
}