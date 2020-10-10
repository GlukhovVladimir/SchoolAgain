using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SchoolAgainBusinessLogic.BindingModels;
using SchoolAgainBusinessLogic.Interfaces;
using SchoolAgainBusinessLogic.ViewModels;
using SchoolAgainDatabaseImplement.Models;

namespace SchoolAgainDatabaseImplement.Implements
{
    public class ServiceLogic : IService
    {
        private readonly string ServiceFileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\123.xml";
        public List<Service> Services { get; set; }
        public ServiceLogic()
        {
            Services = LoadServices();
        }
        private List<Service> LoadServices()
        {
            var list = new List<Service>();
            if (File.Exists(ServiceFileName))
            {
                XDocument xDocument = XDocument.Load(ServiceFileName);
                var xElements = xDocument.Root.Elements("Service").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Service
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ServiceName = elem.Element("ServiceName").Value,
                        Price = Convert.ToInt32(elem.Element("Price").Value),
                    });
                }
            }
            return list;
        }
        public void SaveToDatabase()
        {
            var services = LoadServices();
            using (var context = new SchoolAgainDatabase())
            {
                foreach (var service in services)
                {
                    Service element = context.Services.FirstOrDefault(rec => rec.Id == service.Id);
                    if (element != null)
                    {
                        break;
                    }
                    else
                    {
                        element = new Service();
                        context.Services.Add(element);
                    }
                    element.ServiceName = service.ServiceName;
                    element.Price = service.Price;
                    context.SaveChanges();
                }
            }
        }
        public List<ServiceViewModel> Read(ServiceBindingModel model)
        {
            SaveToDatabase();
            return Services
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new ServiceViewModel
            {
                Id = rec.Id,
                ServiceName = rec.ServiceName,
                Price = rec.Price
            })
            .ToList();
        }
    }
}
