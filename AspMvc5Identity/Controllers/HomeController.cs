using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspMvc5Identity.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public string Index()
        {
            return "Welcome!";
        }
    }
}