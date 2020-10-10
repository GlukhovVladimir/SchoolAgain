using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolAgainBusinessLogic.BindingModels;
using SchoolAgainBusinessLogic.BusinessLogic;
using SchoolAgainBusinessLogic.Enums;
using SchoolAgainBusinessLogic.Interfaces;
using SchoolAgainBusinessLogic.ViewModels;
using SchoolAgainWbClient.Models;


namespace SchoolAgainWbClient.Controllers
{
    public class ReceptionController : Controller
    {
        private readonly IReception _reception;
        private readonly IService _service;
        private readonly IPayment _payment;
        private readonly ReportLogic _report;
        public ReceptionController(IReception reception, IService service, IPayment payment, ReportLogic report)
        {
            _reception = reception;
            _service = service;
            _payment = payment;
            _report = report;
        }

        public IActionResult Reception()
        {
            ViewBag.Receptions = _reception.Read(new ReceptionBindingModel
            {
                ClientId = Program.Client.Id
            });
            return View();
        }
        [HttpPost]
        public IActionResult Reception(ReportModel model)
        {
            var paymentList = new List<PaymentViewModel>();
            var receptions = new List<ReceptionViewModel>();
            receptions = _reception.Read(new ReceptionBindingModel
            {
                ClientId = Program.Client.Id,
                Date = model.From,
                DateTo = model.To
            });
            var payments = _payment.Read(null);
            foreach (var reception in receptions)
            {
                foreach (var payment in payments)
                {
                    if (payment.ClientId == Program.Client.Id && payment.ReceptionId == reception.Id)
                        paymentList.Add(payment);
                }
            }
            ViewBag.Payments = paymentList;
            ViewBag.Receptions = receptions;
            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"gavno.pdf";
            if (model.SendMail)
            {
                _report.SaveReceptionPaymentsToPdfFile(fileName, new ReceptionBindingModel
                {
                    ClientId = Program.Client.Id,
                    Date = model.From,
                    DateTo = model.To
                }, Program.Client.Email);
            }
            return View();
        }
        public IActionResult CreateReception()
        {
            ViewBag.ReceptionServices = _service.Read(null);
            return View();
        }
        [HttpPost]
        public ActionResult CreateReception(CreateReceptionModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReceptionServices = _service.Read(null);
                return View(model);
            }

            if (model.ReceptionServices == null)
            {
                ViewBag.ReceptionServices = _service.Read(null);
                ModelState.AddModelError("", "Услуга не выбрана");
                return View(model);
            }
            var receptionServices = new List<ReceptionServiceBindingModel>();

            foreach (var service in model.ReceptionServices)
            {
                if (service.Value > 0)
                {
                    receptionServices.Add(new ReceptionServiceBindingModel
                    {
                        ServiceId = service.Key,
                        Count = service.Value
                    });
                }
            }
            if (receptionServices.Count == 0)
            {
                ViewBag.Products = _service.Read(null);
                ModelState.AddModelError("", "Услуга не выбрана");
                return View(model);
            }
            _reception.CreateOrUpdate(new ReceptionBindingModel
            {
                ClientId = Program.Client.Id,
                DateCreate = DateTime.Now,
                ReceptionStatus = ReceptionStatus.Оформлен,
                TotalSum = CalculateSum(receptionServices),
                ReceptionServices = receptionServices
            });
            return RedirectToAction("Reception");
        }
        private int CalculateSum(List<ReceptionServiceBindingModel> receptionServices)
        {
            int sum = 0;

            foreach (var service in receptionServices)
            {
                var serviceData = _service.Read(new ServiceBindingModel { Id = service.ServiceId }).FirstOrDefault();

                if (serviceData != null)
                {
                    for (int i = 0; i < service.Count; i++)
                        sum += serviceData.Price;
                }
            }
            return sum;
        }
        public IActionResult Payment(int id)
        {
            var reception = _reception.Read(new ReceptionBindingModel
            {
                Id = id
            }).FirstOrDefault();
            ViewBag.LeftSum = CalculateLeftSum(reception);
            ViewBag.Reception = reception;
            return View();
        }
        [HttpPost]
        public ActionResult Payment(PaymentModel model)
        {
            ReceptionViewModel reception = _reception.Read(new ReceptionBindingModel
            {
                Id = model.ReceptionId
            }).FirstOrDefault();
            int leftSum = CalculateLeftSum(reception);
            if (!ModelState.IsValid)
            {
                ViewBag.Reception = reception;
                ViewBag.LeftSum = leftSum;
                return View(model);
            }
            if (leftSum < model.Sum)
            {
                ViewBag.Reception = reception;
                ViewBag.LeftSum = leftSum;
                return View(model);
            }
            _payment.CreateOrUpdate(new PaymentBindingModel
            {
                ReceptionId = reception.Id,
                ClientId = Program.Client.Id,
                DatePayment = DateTime.Now,
                Sum = model.Sum
            });
            leftSum -= model.Sum;
            _reception.CreateOrUpdate(new ReceptionBindingModel
            {
                Id = reception.Id,
                ClientId = reception.ClientId,
                DateCreate = reception.DateCreate,
                ReceptionStatus = leftSum > 0 ? ReceptionStatus.ОплаченЧастично : ReceptionStatus.Оплачен,
                TotalSum = reception.TotalSum,
                ReceptionServices = reception.ReceptionServices.Select(rec => new ReceptionServiceBindingModel
                {
                    Id = rec.Id,
                    ReceptionId = rec.ReceptionId,
                    ServiceId = rec.ServiceId,
                    Count = rec.Count
                }).ToList()
            });
            return RedirectToAction("Reception");
        }

        private int CalculateLeftSum(ReceptionViewModel reception)
        {       
            int sum = reception.TotalSum;
            int paidSum = _payment.Read(new PaymentBindingModel
            {
                ReceptionId = reception.Id
            }).Select(rec => rec.Sum).Sum();
            return sum - paidSum;
        }
        public IActionResult SendExcelReport(int id)
        {
            var reception = _reception.Read(new ReceptionBindingModel { Id = id }).FirstOrDefault();
            string fileName = "D:\\data\\" + reception.Id + ".xlsx";
            _report.SaveReceptionServicesToExcelFile(fileName, reception, Program.Client.Email);
            return RedirectToAction("Reception");
        }
        public IActionResult SendWordReport(int id)
        {
            var reception = _reception.Read(new ReceptionBindingModel { Id = id }).FirstOrDefault();
            string fileName = "D:\\data\\" + reception.Id + ".docx";
            _report.SaveReceptionServicesToWordFile(fileName, reception, Program.Client.Email);
            return RedirectToAction("Reception");
        }
    }
}