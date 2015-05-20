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
        public void Show(int id)
        {
            byte[] image = context.RegisteredUser.Where(x => x.IDreguser == id).SingleOrDefault().avatar;
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "image";
            Response.BinaryWrite(image);
            Response.End();
        }


    }
}