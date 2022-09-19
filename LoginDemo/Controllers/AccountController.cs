using LoginDemo.Models;
using LoginDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Security;

namespace LoginDemo.Controllers
{
    public class AccountController : Controller
    {

        Repository repository = new Repository();
        // GET: Account
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginViewModel model) //Login
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var result = await repository.login(model);
            if (result.resultCode == 200)
            {                
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                ModelState.AddModelError("", result.message);
            }

            return View();
        }


        public ActionResult Register() //Register
        {
            return View();
        }



        public ActionResult Logout() 
        {
            SignOut();

            return RedirectToAction("Index"); 
        }


        [HttpPost]
        public async Task<ActionResult> Register(LoginViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var result = await repository.Register(model);

            if (result.resultCode == 200)
            {
                return RedirectToAction("Index"); //redirect to login form
            }
            else
            {
                ModelState.AddModelError("", result.message);
            }

            return View();
        }
               
    }
}