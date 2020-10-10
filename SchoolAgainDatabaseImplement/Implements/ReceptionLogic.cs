using Microsoft.EntityFrameworkCore;
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
    public class ReceptionLogic : IReception
    {
        public void CreateOrUpdate(ReceptionBindingModel model)
        {
            using (var context = new SchoolAgainDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Reception element = model.Id.HasValue ? null : new Reception();
                        if (model.Id.HasValue)
                        {
                            element = context.Receptions.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                            element.ClientId = model.ClientId;
                            element.DateCreate = model.DateCreate;
                            element.TotalSum = model.TotalSum;
                            element.ReceptionStatus = model.ReceptionStatus;
                            context.SaveChanges();
                        }
                        else
                        {
                            element.ClientId = model.ClientId;
                            element.DateCreate = model.DateCreate;
                            element.TotalSum = model.TotalSum;
                            element.ReceptionStatus = model.ReceptionStatus;
                            context.Receptions.Add(element);
                            context.SaveChanges();
                            var groupServices = model.ReceptionServices
                               .GroupBy(rec => rec.ServiceId)
                               .Select(rec => new
                               {
                                   ServiceId = rec.Key,
                                   Count = rec.Sum(r => r.Count)
                               });

                            foreach (var groupService in groupServices)
                            {
                                context.ReceptionServices.Add(new ReceptionService
                                {
                                    ReceptionId = element.Id,
                                    ServiceId = groupService.ServiceId,
                                    Count = groupService.Count
                                });
                                context.SaveChanges();
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(ReceptionBindingModel model)
        {
            using (var context = new SchoolAgainDatabase())
            {
                Reception element = context.Receptions.FirstOrDefault(rec => rec.Id == model.Id.Value);

                if (element != null)
                {
                    context.Receptions.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<ReceptionViewModel> Read(ReceptionBindingModel model)
        {
            using (var context = new SchoolAgainDatabase())
            {
                return context.Receptions.Where(rec => rec.Id == model.Id 
                || (rec.ClientId == model.ClientId) 
                && (model.Date == null 
                && model.DateTo == null 
                || rec.DateCreate >= model.Date 
                && rec.DateCreate <= model.DateTo))
                .Select(rec => new ReceptionViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    ClientFIO = rec.Client.ClientFIO,
                    TotalSum = rec.TotalSum,
                    DateCreate = rec.DateCreate,
                    LeftSum = rec.TotalSum - context.Payments.Where(recP => recP.ReceptionId == rec.Id).Select(recP => recP.Sum).Sum(),
                    ReceptionStatus = rec.ReceptionStatus,
                    ReceptionServices = GetReceptionServiceViewModel(rec)
                })
            .ToList();
            }
        }
        public static List<ReceptionServiceViewModel> GetReceptionServiceViewModel(Reception reception)
        {
            using (var context = new SchoolAgainDatabase())
            {
                var ReceptionServices = context.ReceptionServices
                    .Where(rec => rec.ReceptionId == reception.Id)
                    .Include(rec => rec.Service)
                    .Select(rec => new ReceptionServiceViewModel
                    {
                        Id = rec.Id,
                        ReceptionId = rec.ReceptionId,
                        ServiceId = rec.ServiceId,
                        Count = rec.Count
                    }).ToList();
                foreach (var service in ReceptionServices)
                {
                    var serviceData = context.Services.Where(rec => rec.Id == service.ServiceId).FirstOrDefault();
                    service.ServiceName = serviceData.ServiceName;
                    service.Price = serviceData.Price;
                }
                return ReceptionServices;
            }
        }
    }
}
