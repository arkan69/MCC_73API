using Client.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        

        //[HttpPost("/Login")]
        //public IActionResult Login(LoginVM login)
        //{
        //    var result = _repo.Login(login);
        //    if (result == 2)
        //    {
        //        HttpContext.Session.SetString("email", login.Email);
        //        HttpContext.Session.SetString("role", _repo.UserRoles(login.Email).FirstOrDefault());
        //        if (HttpContext.Session.GetString("role") == "Manager")
        //        {
        //            return RedirectToAction("Index", "Department");
        //        }
        //        return RedirectToAction("Index", "Home");
        //    }
        //    ViewBag.error = "Login Failed";
        //    return View();
        //}

    }
}
