using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using My.AppStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace My.AppStore.Controllers
{
    public class MyAccountController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                using (IdentityModels entities = new IdentityModels())
                {

                    var userStore = new UserStore<User>(entities);

                    var manager = new UserManager<User>(userStore);

                    var user = new User()
                    {
                        UserName = model.EmailAddress,
                        Email = model.EmailAddress,
                        EmailConfirmed = true
                    };

                    IdentityResult result = manager.Create(user, model.Password);
                    if (result.Succeeded)
                    {
                        FormsAuthentication.SetAuthCookie(model.EmailAddress, true);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("EmailAddress", "Unable to register with this email address");
                    }
                }

            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                using (IdentityModels entities = new IdentityModels())
                {

                    var userStore = new UserStore<User>(entities);

                    var manager = new UserManager<User>(userStore);

                    var user = manager.FindByEmail(model.EmailAddress);
                    if (manager.CheckPassword(user, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.EmailAddress, true);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("EmailAddress", "Invalid password and/or username.");
                }
            }
            return View(model);
        }
    }
}