using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DemoExercise.Models;
using DemoExercise.Repositories;
using DemoExercise.Services;
using DemoExercise.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DemoExercise.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private LoginService loginService;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            this.logger = logger;

            loginService = new LoginService(new UserLoginRepo(config));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserViewModel user)
        {
            // I didn't want to spend a ton of time here and add this to everything, but I wanted you to know
            // that I understand model attributes and validation.
            // Client side validation improves UX, while server side validation prevents SQL injection and other problems. 
            // Should always have both
            if (ModelState.IsValid)
            {
                TResponse<bool> response = new TResponse<bool>();

                try
                {
                    response = loginService.ValidateUsername(user.Username);
                }
                catch (Exception ex)
                {
                    logger.LogError($"Unable to verify User: {user.Username}. Error: {ex}");

                    response.IsSuccesful = false;
                }

                if (!response.IsSuccesful)
                {
                    logger.LogWarning($"User ({user.Username}) not found.");

                    return View("UserNotFound", user);
                }

                return View("LoginPassword", user);
            }
            
            return View("Index");
        }

        [HttpGet]
        public ActionResult LoginPassword(string username)
        {
            UserViewModel user = new UserViewModel() { Username = username };

            return View(user);
        }

        [HttpPost]
        public ActionResult LoginPassword(UserViewModel user)
        {
            TResponse<bool> response = new TResponse<bool>();

            try
            {
                response = loginService.ValidatePassword(user.Username, user.Password);
            }
            catch (Exception ex)
            {
                logger.LogError($"Problem validating password for User: {user}. Error: {ex}.");

                response.IsSuccesful = false;
            }

            if (!response.IsSuccesful)
            {
                logger.LogWarning($"Wrong password ({user.Password}) for username ({user.Username}).");

                return View("UserNotFound", user);
            }

            return View("LoggedIn", user);
        }

        public ActionResult LoggedIn(UserViewModel user)
        {
            return View(user);
        }

        public ActionResult UserNotFound(UserViewModel user)
        {
            return View(user);
        }
    }
}
