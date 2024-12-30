using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EmailClient.Models.ApiServices;
using EmailClient.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailClient.Controllers
{
    [Route("[controller]")]
    public class User1Controller : Controller
    {
        public readonly User1ApiServices user1ApiServices;

        public User1Controller(User1ApiServices user1ApiServices)
        {
            this.user1ApiServices = user1ApiServices;
        }
        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            var model = new View_models
            {
                user1 = new User1(),
                verifyOtpRequest = new VerifyOtpRequest()
            };
            return View(model);
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(User1 user1)
        {

            if (ModelState.IsValid)
            {
                HttpResponseMessage response = user1ApiServices.register(user1);
                string data = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("verifyotp");
                    
                }
                else
                {
                    ViewBag.msg = data;
                }
            }
            return View();
        }


        [Route("verifyOtp")]
        public IActionResult verifyOtp(VerifyOtpRequest verifyOtpRequest)
        {

            if (ModelState.IsValid)
            {
                HttpResponseMessage response = user1ApiServices.verifyotp(verifyOtpRequest);
                string data = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.msg = data;
                }
                else
                {
                    ViewBag.msg = data;
                }
            }
            return View();
        }


    }
}
