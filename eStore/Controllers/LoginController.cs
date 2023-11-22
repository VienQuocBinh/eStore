using BusinessObject;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eStore.Controllers
{
    public class LoginController : Controller
    {
        private static string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectionString = configuration.GetConnectionString("Database");
            return connectionString;
        }

        IMemberRepo repo = new MemberRepo(GetConnectionString());


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string adminEmail = config.GetSection("admin").GetSection("email").Value;
            string adminPassword = config.GetSection("admin").GetSection("password").Value;

            if (email == null || password == null)
            {
                TempData["msg"] = "Login failed! Please try again";
                return View();
            }
            else if (email.Equals(adminEmail) && password.Equals(adminPassword))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Member member = repo.Login(email, password);
                if (member == null)
                {
                    TempData["msg"] = "Login failed! Please try again";
                    return View();
                }
                else
                {
                    HttpContext.Session.SetString("email", email);
                    return Redirect("/Members/UserDetail");
                }
            }
        }

        public IActionResult Logout()
        {
            TempData.Clear();
            HttpContext.Session.Clear();
            return Redirect("/Login/Login");
        }

    }
}
