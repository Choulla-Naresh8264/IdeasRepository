using System.Web.Mvc;

namespace IdeasRepository.Web.Controllers
{
    /// <summary>
    /// Describes the necessary actions to display basic information about the project.
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }
    }
}
