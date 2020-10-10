using System;
using System.Collections.Generic;
using System.Text;
using SchoolAgainBusinessLogic.BindingModels;
using SchoolAgainBusinessLogic.ViewModels;

namespace SchoolAgainBusinessLogic.Interfaces
{
    public interface IService
    {
        List<ServiceViewModel> Read(ServiceBindingModel model);
    }
}
