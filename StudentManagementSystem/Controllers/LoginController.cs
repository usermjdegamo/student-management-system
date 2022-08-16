using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        Database1Entities db = new Database1Entities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(user userObj)
        {
            if (ModelState.IsValid)
            {
                using (Database1Entities db = new Database1Entities())
                {
                    var obj = db.users.Where(a => a.username.Equals(userObj.username) && a.password.Equals(userObj.password)).FirstOrDefault();

                    if (obj != null)
                    {
                        Session["UserID"] = obj.Id.ToString();
                        Session["UserName"] = obj.username.ToString();
                        Session["Password"] = obj.password.ToString();
                        return RedirectToAction("Index", "Home");
                    } else
                    {
                        ModelState.AddModelError("", "The username/password is incorrect");
                    }
                }
            }            

            return View(userObj);
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index", "Login");
        }
    }
}