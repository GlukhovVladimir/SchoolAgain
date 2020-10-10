using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using SchoolAgainBusinessLogic.BindingModels;
using SchoolAgainBusinessLogic.HelperModels;
using SchoolAgainBusinessLogic.Interfaces;
using SchoolAgainBusinessLogic.ViewModels;

namespace SchoolAgainBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly IService serviceLogic;
        private readonly IReception receptionLogic;
        private readonly IPayment paymentLogic;
        public ReportLogic(IService serviceLogic, IReception receptionLogic, IPayment paymentLogic)
        {
            this.serviceLogic = serviceLogic;
            this.receptionLogic = receptionLogic;
            this.paymentLogic = paymentLogic;
        }
        public List<ServiceViewModel> GetReceptionServices(ReceptionViewModel reception)
        {
            var services = new List<ServiceViewModel>();

            foreach (var service in reception.ReceptionServices)
            {
                services.Add(serviceLogic.Read(new ServiceBindingModel
                {
                    Id = service.ServiceId
                }).FirstOrDefault());

            }
            return services;
        }
        public Dictionary<int, List<PaymentViewModel>> GetReceptionPayments(ReceptionBindingModel model)
        {
            var receptions = receptionLogic.Read(model).ToList();
            Dictionary<int, List<PaymentViewModel>> payments = new Dictionary<int, List<PaymentViewModel>>();
            foreach (var reception in receptions)
            {
                var receptionPayments = paymentLogic.Read(new PaymentBindingModel { ReceptionId = reception.Id }).ToList();
                payments.Add(reception.Id, receptionPayments);
            }
            return payments;
        }
        public void SaveReceptionPaymentsToPdfFile(string fileName, ReceptionBindingModel reception, string email)
        {
            string title = "Список записей в период с " + reception.Date.ToString() + " по " + reception.DateTo.ToString();
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = fileName,
                Title = title,
                Receptions = receptionLogic.Read(reception).ToList(),
                Payments = GetReceptionPayments(reception)
            });
            SendMail(email, fileName, title);
        }
        public void SaveReceptionServicesToWordFile(string fileName, ReceptionViewModel reception, string email)
        {
            string title = "Список кружков по записи №" + reception.Id;
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = fileName,
                Title = title,
                Services = GetReceptionServices(reception)
            });
            SendMail(email, fileName, title);
        }
        public void SaveReceptionServicesToExcelFile(string fileName, ReceptionViewModel reception, string email)
        {
            string title = "Список кружков по записи №" + reception.Id;
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = fileName,
                Title = title,
                Services = GetReceptionServices(reception)
            });
            SendMail(email, fileName, title);
        }
        public void SendMail(string email, string fileName, string subject)
        {
            MailAddress from = new MailAddress("acclaba.7@gmail.com", "Школа Опять учиться");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Attachments.Add(new Attachment(fileName));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("acclaba.7@gmail.com", "123456789x$");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
