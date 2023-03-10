using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using UIPresantation.Models;
using UIPresantation.ApiHelper;

namespace UIPresantation.Controllers
{
    public class LoginController : Controller
    {
        IHttpClientFactory httpClientFactory;
        public LoginController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("token") != null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserForLoginVM userForLoginVM)
        {
            AccessTokenVM accessTokenVM = new AccessTokenVM();
            ApiHelper.ApiHelper apiHelper = new ApiHelper.ApiHelper(httpClientFactory);
            
            if (!string.IsNullOrEmpty(userForLoginVM.Email) && string.IsNullOrEmpty(userForLoginVM.Password))
            {
                return RedirectToAction("Login");
            }

            accessTokenVM = await apiHelper.UserLogin(userForLoginVM);
            if (accessTokenVM == null)
            {
                TempData["message"] = "İşlem Esnasında Hata alınmıştır. Lütfen Tekrar Deneyiniz.";
                return View("Login");
            }

            HttpContext.Session.SetString("token", accessTokenVM.Token);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserRegister(UserForRegisterVM userForRegisterVM)
        {
            AccessTokenVM accessTokenVM = new AccessTokenVM();
            ApiHelper.ApiHelper apiHelper = new ApiHelper.ApiHelper(httpClientFactory);

            accessTokenVM = await apiHelper.UserAdd(userForRegisterVM);
            if (accessTokenVM == null)
            {
                TempData["message"] = "İşlem Esnasında Hata alınmıştır. Lütfen Tekrar Deneyiniz.";
                return View("Register");
            }
            HttpContext.Session.GetString("token");
            HttpContext.Session.SetString("token", accessTokenVM.Token);
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }
    }
}
