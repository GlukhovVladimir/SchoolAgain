using System;
using System.Collections.Generic;
using System.Text;
using SchoolAgainBusinessLogic.BindingModels;
using SchoolAgainBusinessLogic.ViewModels;

namespace SchoolAgainBusinessLogic.Interfaces
{
    public interface IPayment
    {
        List<PaymentViewModel> Read(PaymentBindingModel model);
        void CreateOrUpdate(PaymentBindingModel model);
        void Delete(PaymentBindingModel model);
    }
}
