using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolAgainBusinessLogic.BindingModels;
using SchoolAgainBusinessLogic.Interfaces;
using SchoolAgainWbClient.Models;

namespace SchoolAgainWbClient.Controllers
{
    public class AdminController : Controller
    {
        private string password = "qwerty";
        private readonly IClient _client;

        public AdminController(IClient client)
        {
            _client = client;
        }
        public IActionResult Index(AdminModel model)
        {
            if (model.Password == password)
            {
                Program.AdminModel = !Program.AdminModel;
                return RedirectToAction("Blocking");
            }
            else if (model.Password != password && model.Password != null)
            {
                ModelState.AddModelError("", "Вы ввели неверный пароль");
                return View();
            }
            return View();
        }
        public IActionResult Blocking()
        {
            ViewBag.Clients = _client.Read(null);
            return View();
        }
        public ActionResult Block(int id)
        {
            if (ModelState.IsValid)
            {
                var existClient = _client.Read(new ClientBindingModel
                {
                    Id = id
                }).FirstOrDefault();
                _client.CreateOrUpdate(new ClientBindingModel
                {
                    Id = id,
                    ClientFIO = existClient.ClientFIO,
                    Login = existClient.Login,
                    Password = existClient.Password,
                    Email = existClient.Email,
                    Phone = existClient.Phone,
                    Block = !existClient.Block
                });
                return RedirectToAction("Blocking");
            }
            return RedirectToAction("Blocking");
        }
    }
}