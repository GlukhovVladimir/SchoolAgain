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
    public class KidLogic : IKid
    {
        public void CreateOrUpdate(KidBindingModel model)
        {
            using (var context = new SchoolAgainDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Kid element = model.Id.HasValue ? null : new Kid();
                        if (model.Id.HasValue)
                        {
                            element = context.Kids.FirstOrDefault(rec => rec.ClientId ==
                           model.ClientId && rec.KidName == model.KidName);
                            if (element == null)
                            {
                                throw new Exception("Такой питомец уже существует");
                            }
                            element.KidName = model.KidName;
                            element.Height = model.Height;
                            element.Free = model.Free;
                            element.Age = model.Age;
                            element.Gender = model.Gender;
                            element.ClientId = model.ClientId;
                            context.SaveChanges();
                        }
                        else
                        {
                            element.KidName = model.KidName;
                            element.Height = model.Height;
                            element.Free = model.Free;
                            element.Age = model.Age;
                            element.Gender = model.Gender;
                            element.ClientId = model.ClientId;
                        }
                        context.Kids.Add(element);
                        context.SaveChanges();

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
        public void Delete(KidBindingModel model)
        {
            using (var context = new SchoolAgainDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Kid element = context.Kids.FirstOrDefault(rec => rec.Id == model.Id.Value);

                        if (element != null)
                        {
                            context.ClientKids.RemoveRange(
                                    context.ClientKids.Where(
                                        rec => rec.KidId == model.Id.Value));
                            context.Kids.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
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
        public List<KidViewModel> Read(KidBindingModel model)
        {
            using (var context = new SchoolAgainDatabase())
            {
                return context.Kids
                 .Where(rec => model == null
                   || rec.Id == model.Id
                 || (rec.ClientId == model.ClientId))
            .Select(rec => new KidViewModel
            {
                Id = rec.Id,
               ClientId =rec.ClientId,
                Height = rec.Height,
                KidName = rec.KidName,
                Free = rec.Free,
                Age = rec.Age,
                Gender = rec.Gender,
                ClientKids = GetClientKidViewModel(rec)
            })
                .ToList();
            }
        }
        public static List<ClientKidViewModel> GetClientKidViewModel(Kid kid)
        {
            using (var context = new SchoolAgainDatabase())
            {
                var ClientKids = context.ClientKids
                    .Where(rec => rec.KidId == kid.Id)
                    .Include(rec => rec.Client)
                    .Select(rec => new ClientKidViewModel
                    {
                        Id = rec.Id,
                        KidId = rec.KidId,
                        ClientId = rec.ClientId,
                        Count = rec.Count
                    }).ToList();
                foreach (var client in ClientKids)
                {
                    var clientData = context.Clients.Where(rec => rec.Id == client.ClientId).FirstOrDefault();
                    client.ClientFIO = clientData.ClientFIO;

                }
                return ClientKids;
            }
        }
    }
}
