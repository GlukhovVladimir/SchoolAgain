using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolAgainBusinessLogic.BindingModels;
using SchoolAgainBusinessLogic.Interfaces;
using SchoolAgainBusinessLogic.ViewModels;
using SchoolAgainDatabaseImplement;
using SchoolAgainWbClient.Models;

namespace SchoolAgainWbClient.Controllers
{
    public class KidController : Controller
    {
        private readonly IKid _kid;
        public KidController(IKid kid)
        {
            _kid = kid;
        }
        public IActionResult Kid()
        {
            ViewBag.Kids = _kid.Read(new KidBindingModel
            {
                ClientId = Program.Client.Id
            });
            return View();
        }
        public ActionResult ProfileKid()
        {
            ViewBag.Kids = _kid.Read(null);
            return View();

        }
        [HttpPost]
        public ActionResult Kid(KidModel kid)
        {
            if (!ModelState.IsValid)
                {
                    ViewBag.ClientKids = _kid.Read(null);
                    return View(kid);
                }
            _kid.CreateOrUpdate(new KidBindingModel
                {
                    ClientId = Program.Client.Id,
                    KidName = kid.KidName,
                    Height = kid.Height,
                    Free = kid.Free,
                    Age = kid.Age,
                    Gender = kid.Gender
                });
                ModelState.AddModelError("", "Вы успешно добавили питомца");
               return RedirectToAction("ProfileKid");
        }
    }
}



