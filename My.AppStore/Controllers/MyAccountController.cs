using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using My.AppStore.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace My.AppStore.Controllers
{
    public class MyAccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View(new RegisterModel());
        }

        //SendGrid & Braintree stuff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                using (IdentityModels entities = new IdentityModels())
                {

                    var userStore = new UserStore<User>(entities);

                    var manager = new UserManager<User>(userStore);
                    manager.UserTokenProvider = new EmailTokenProvider<User>();

                    var user = new User()
                    {
                        UserName = model.EmailAddress,
                        Email = model.EmailAddress
                    };

                    IdentityResult result = manager.Create(user, model.Password);

                    if (result.Succeeded)
                    {
                        User u = manager.FindByName(model.EmailAddress);

                        // Creates customer record in Braintree
                        string merchantId = ConfigurationManager.AppSettings["Braintree.MerchantID"];
                        string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
                        string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
                        string environment = ConfigurationManager.AppSettings["Braintree.Environment"];
                        Braintree.BraintreeGateway braintree = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);
                        Braintree.CustomerRequest customer = new Braintree.CustomerRequest();
                        customer.CustomerId = u.Id;
                        customer.Email = u.Email;

                        var r = await braintree.Customer.CreateAsync(customer);

                        string confirmationToken = manager.GenerateEmailConfirmationToken(u.Id);

                        string sendGridApiKey = ConfigurationManager.AppSettings["SendGrid.ApiKey"];

                        SendGrid.SendGridClient client = new SendGrid.SendGridClient(sendGridApiKey);
                        SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage();
                        message.Subject = string.Format("Please confirm your account");
                        message.From = new SendGrid.Helpers.Mail.EmailAddress("admin@apps.willmabrey.com", "Will Mabrey");
                        message.AddTo(new SendGrid.Helpers.Mail.EmailAddress(model.EmailAddress));
                        SendGrid.Helpers.Mail.Content contents = new SendGrid.Helpers.Mail.Content("text/html", string.Format("<a href=\"{0}\">Confirm Account</a>", Request.Url.GetLeftPart(UriPartial.Authority) + "/MyAccount/Confirm/" + confirmationToken + "?email=" + model.EmailAddress));

                        message.AddContent(contents.Type, contents.Value);
                        SendGrid.Response response = await client.SendEmailAsync(message);

                        return RedirectToAction("ConfirmSent");
                    }
                    else
                    {
                        ModelState.AddModelError("EmailAddress", "Unable to register with this email address.");
                    }
                }

            }
            return View(model);
        }

        public ActionResult ConfirmSent()
        {
            return View();
        }

        public ActionResult Confirm(string id, string email)
        {
            using (IdentityModels entities = new IdentityModels())
            {
                var userStore = new UserStore<User>(entities);

                var manager = new UserManager<User>(userStore);
                manager.UserTokenProvider = new EmailTokenProvider<User>();
                var user = manager.FindByName(email);
                if (user != null)
                {
                    var result = manager.ConfirmEmail(user.Id, id);
                    if (result.Succeeded)
                    {
                        TempData.Add("AccountConfirmed", true);
                        return RedirectToAction("Login");
                    }
                }
            }

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
                        //Something similar to this should redirect the user to the Create Review page once they log in. TempData likely not the way to go, unless you perhaps
                        //changed it. Sam mentioned using Filters, global filters just something like that
                        //if (TempData["ReviewAttempted"] != null)
                        //{
                        //    return RedirectToAction("Create", "Reviews", new { name = TempData["ThisProductName"], id = TempData["ThisProductID"] });
                        //}
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("EmailAddress", "Invalid username and/or password.");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            if (Request.Cookies["orderNumber"] != null)
            {
                var c = new HttpCookie("orderNumber");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Ok()
        {
            return View();
        }

        public ActionResult ResetSent()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        public ActionResult ResetPassword(string id, string EmailAddress)
        {
            return View(new ResetPasswordViewModel(id, EmailAddress));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (IdentityModels entities = new IdentityModels())
                {

                    var userStore = new UserStore<User>(entities);
                    var manager = new UserManager<User>(userStore);

                    manager.UserTokenProvider = new EmailTokenProvider<User>();

                    var user = manager.FindByName(model.Email);
                    // If user has to activate his email to confirm his account, the use code listing below
                    //if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                    //{
                    //    return Ok();
                    //}


                    string code = await manager.GeneratePasswordResetTokenAsync(user.Id);


                    string sendGridKey = System.Configuration.ConfigurationManager.AppSettings["SendGrid.ApiKey"];
                    SendGrid.SendGridClient client = new SendGrid.SendGridClient(sendGridKey);
                    SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage();
                    message.Subject = string.Format("Reset Password");
                    message.From = new SendGrid.Helpers.Mail.EmailAddress("will@willmabrey.com", "Will Mabrey");
                    message.AddTo(new SendGrid.Helpers.Mail.EmailAddress(model.Email));

                    SendGrid.Helpers.Mail.Content contents = new SendGrid.Helpers.Mail.Content("text/html", string.Format("<a href=\"{0}\">Reset Password</a>", Request.Url.GetLeftPart(UriPartial.Authority) + "/MyAccount/ResetPassword/" + code + "?EmailAddress=" + model.Email));

                    message.AddContent(contents.Type, contents.Value);
                    SendGrid.Response response = await client.SendEmailAsync(message);

                    //await client.SendEmailAsync(user.Id, "Reset Password", $"Please reset your password by using this {code}");
                    return RedirectToAction("ResetSent");
                }

            }
            return View();

            // If we got this far, something failed, redisplay form

        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            using (IdentityModels entities = new IdentityModels())
            {
                var userStore = new UserStore<User>(entities);

                var manager = new UserManager<User>(userStore);
                var user = manager.FindByName(model.EmailAddress);

                manager.UserTokenProvider = new EmailTokenProvider<User>();

                if (user != null)
                {
                    var result = manager.ResetPassword(user.Id, model.Code, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }

                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}