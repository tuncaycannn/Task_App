using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UIPresantation.Models;

namespace UIPresantation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IHttpClientFactory httpClientFactory;
        
        public HomeController(IHttpClientFactory httpClientFactory, ILogger<HomeController> logger)
        {
            this.httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            UserResponseVM userResponseVM = new UserResponseVM();

            ApiHelper.ApiHelper apiHelper = new ApiHelper.ApiHelper(httpClientFactory);

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                return RedirectToAction("Login", "Login");
            }

            userResponseVM = await apiHelper.GetAllUser();

            return View(userResponseVM);
        }

        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserDelete(int id)
        {
            UserDeleteVM userDeleteVM= new UserDeleteVM();
            GetByIdUserResponseVM userResponseVM = new GetByIdUserResponseVM();
            ApiHelper.ApiHelper apiHelper = new ApiHelper.ApiHelper(httpClientFactory);

            userResponseVM = await apiHelper.GetUserById(id);
            if (userResponseVM.Data == null)
            {
                TempData["message"] = string.Format("{0} ID'li Kullanıcı Yoktur", id);
                return View("Delete");
            }

            string jwt = HttpContext.Session.GetString("token");
            userDeleteVM =  await apiHelper.UserDelete(id, jwt);
            if (userDeleteVM == null)
            {
                TempData["message"] = "İşlem için Yetkiniz Yoktur! Sadece Admin kullanıcısı ile kayıt silebilirsiniz.";
            }

            return View("Delete");
        }

        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserUpdate(int id, string email, string firstName, string LastName)
        {
            GetByIdUserResponseVM userResponseVM = new GetByIdUserResponseVM() { Data = new UserVM { Id = id, Email = email, FirstName = firstName, LastName = LastName, Status = true } };
            UserForUpdate userForUpdate = new UserForUpdate();
            ApiHelper.ApiHelper apiHelper = new ApiHelper.ApiHelper(httpClientFactory);

            userForUpdate = await apiHelper.UserUpdate(userResponseVM.Data);
            if (userForUpdate == null)
            {
                TempData["message"] = "İşlem Esnasında Hata Oluşmuştur. Lütfen Tekrar Deneyin.";
                return View("Update");
            }
            return View("Update");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFromUser(int id)
        {
            GetByIdUserResponseVM userResponseVM = new GetByIdUserResponseVM();

            ApiHelper.ApiHelper apiHelper = new ApiHelper.ApiHelper(httpClientFactory);

            userResponseVM = await apiHelper.GetUserById(id);
            if (userResponseVM.Data == null)
            {
                TempData["message"] = string.Format("{0} ID'li Kullanıcı Yoktur", id);
                return View("Update");
            }

            return View("Update", userResponseVM);
        }
        public IActionResult GetById()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserGetById(int id)
        {
            GetByIdUserResponseVM userResponseVM = new GetByIdUserResponseVM();

            ApiHelper.ApiHelper apiHelper = new ApiHelper.ApiHelper(httpClientFactory);

            userResponseVM = await apiHelper.GetUserById(id);
            if (userResponseVM.Data == null)
            {
                TempData["message"] = string.Format("{0} ID'li Kullanıcı Yoktur", id);
                return View("GetById");
            }

            return View("GetById", userResponseVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
