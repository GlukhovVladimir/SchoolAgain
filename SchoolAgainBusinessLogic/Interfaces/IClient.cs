using System;
using System.Collections.Generic;
using System.Text;
using SchoolAgainBusinessLogic.BindingModels;
using SchoolAgainBusinessLogic.ViewModels;

namespace SchoolAgainBusinessLogic.Interfaces
{
    public interface IClient
    {
        List<ClientViewModel> Read(ClientBindingModel model);
        void CreateOrUpdate(ClientBindingModel model);
        void Delete(ClientBindingModel model);
    }
}
