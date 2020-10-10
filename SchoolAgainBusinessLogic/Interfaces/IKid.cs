using System;
using System.Collections.Generic;
using System.Text;
using SchoolAgainBusinessLogic.BindingModels;
using SchoolAgainBusinessLogic.ViewModels;

namespace SchoolAgainBusinessLogic.Interfaces
{
    public interface IKid
    {
        List<KidViewModel> Read(KidBindingModel model);
        void CreateOrUpdate(KidBindingModel model);
        void Delete(KidBindingModel model);
    }
}
