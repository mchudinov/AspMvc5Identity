using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AspMvc5Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace AspMvc5Identity.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IAuthenticationManager AuthManager => HttpContext.GetOwinContext().Authentication;
        private AppUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new [] { "Access Denied. Already logged in." });
            }
            return View();
        }        

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel details)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindAsync(details.Name, details.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid name or password.");
                }
                else
                {
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties{IsPersistent = false}, ident);
                    return RedirectToAction("Index", "UserAdmin");
                }
            }
            return View(details);
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}