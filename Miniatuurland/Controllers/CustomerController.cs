using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Miniatuurland.Models;
using Miniatuurland.Services;

namespace Miniatuurland.Controllers
{
    public class CustomerController : Controller
    {
        private ProductService service = new ProductService();

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        //GET: Customer/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //POST: Customer/Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            //username en password vanuit View meegekregen ("name" properties van de form-elementen)
            //worden door de DefaultModelBinder automatisch doorgegeven aan de controller, mits gebruik te maken van de juiste benaming
            var customer = service.GetCustomerByUsernameAndPassword(username, password);
            if (customer != null)
            {
                Session["customer"] = customer;
                TempData.Remove("loginError");
                return RedirectToAction("Index", "Product");
            }
            else
            {
                //this.ModelState.AddModelError(string.Empty, "login: " + username + " or password are not valid");
                //ViewBag.errorcount = ModelState.Values.Count();
                TempData["loginError"] = "login: " + username + " or password are not valid";
                this.ModelState.AddModelError(String.Empty, TempData["loginError"].ToString());
                ViewBag.errorcount = ModelState.Values.Count();
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session["customer"] = null;
            return RedirectToAction("Index", "Product");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel viewmodel)
        {
            if (this.ModelState.IsValid)
            {
                var customer = new Customer
                {
                    name = viewmodel.name,
                    address = viewmodel.address,
                    city = viewmodel.city,
                    state = viewmodel.state,
                    postalCode = viewmodel.postalCode,
                    username = viewmodel.username,
                    password = viewmodel.password,
                    email = viewmodel.email
                };                
                bool registrationOk = service.RegisterUser(customer);

                //registration = login
                if ((registrationOk))
                {
                    Session["customer"] = customer;
                }

                return RedirectToAction("ConfirmRegistration","Customer",new { registrationOk = registrationOk});
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, "No registration");
                ViewBag.errorcount = ModelState.Values.Count();
                return View(viewmodel);
            }
        }
        public ActionResult ConfirmRegistration(bool registrationOk)
        {
            ViewBag.registrationOk = registrationOk;            
            return View();
        }
    }
}