using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace AspMvc5Identity.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        private AppUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
    }
}