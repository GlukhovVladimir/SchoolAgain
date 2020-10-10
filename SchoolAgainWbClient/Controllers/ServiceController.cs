using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolAgainBusinessLogic.Interfaces;

namespace SchoolAgainWbClient.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IService _service;
        public ServiceController(IService service)
        {
            _service = service;
        }
        public IActionResult Service()
        {
            ViewBag.Services = _service.Read(null);
            return View();
        }
    }
}