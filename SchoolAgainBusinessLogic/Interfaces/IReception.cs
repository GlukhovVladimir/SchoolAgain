using System;
using System.Collections.Generic;
using System.Text;
using SchoolAgainBusinessLogic.BindingModels;
using SchoolAgainBusinessLogic.ViewModels;

namespace SchoolAgainBusinessLogic.Interfaces
{
    public interface IReception
    {
        List<ReceptionViewModel> Read(ReceptionBindingModel model);
        void CreateOrUpdate(ReceptionBindingModel model);
        void Delete(ReceptionBindingModel model);
    }
}
