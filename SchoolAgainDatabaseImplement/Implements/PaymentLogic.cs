using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SchoolAgainBusinessLogic.BindingModels;
using SchoolAgainBusinessLogic.Interfaces;
using SchoolAgainBusinessLogic.ViewModels;
using SchoolAgainDatabaseImplement.Models;

namespace SchoolAgainDatabaseImplement.Implements
{
    public class PaymentLogic : IPayment
    {
        public void CreateOrUpdate(PaymentBindingModel model)
        {
            using (var context = new SchoolAgainDatabase())
            {
                Payment element = model.Id.HasValue ? null : new Payment();
                if (model.Id.HasValue)
                {
                    element = context.Payments.FirstOrDefault(rec => rec.Id ==
                   model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Payment();
                    context.Payments.Add(element);
                }
                element.ReceptionId = model.ReceptionId;
                element.ClientId = model.ClientId;
                element.Sum = model.Sum;
                element.DatePayment = model.DatePayment;
                context.SaveChanges();
            }
        }
        public void Delete(PaymentBindingModel model)
        {
            using (var context = new SchoolAgainDatabase())
            {
                Payment element = context.Payments.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Payments.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<PaymentViewModel> Read(PaymentBindingModel model)
        {
            using (var context = new SchoolAgainDatabase())
            {
                return context.Payments
                .Where(rec => model == null || rec.Id == model.Id || rec.ReceptionId.Equals(model.ReceptionId))
                .Select(rec => new PaymentViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    DatePayment = rec.DatePayment,
                    ReceptionId = rec.ReceptionId,
                    Sum = rec.Sum
                })
                .ToList();
            }
        }
    }
}
