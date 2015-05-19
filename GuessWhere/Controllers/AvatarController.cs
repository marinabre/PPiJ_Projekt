using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using GuessWhere.Models;

namespace GuessWhere.Controllers
{
    [Authorize]
    public class AvatarController : Controller
    {
        Guess_WhereEntities1 context;

        // GET: Avatar
        public ActionResult Index()
        {
            return View();
        }
    }
}