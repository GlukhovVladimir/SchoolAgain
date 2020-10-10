using System;
using System.Collections.Generic;
using System.Text;
using SchoolAgainBusinessLogic.ViewModels;

namespace SchoolAgainBusinessLogic.HelperModels
{
    public class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReceptionViewModel> Receptions { get; set; }
        public Dictionary<int, List<PaymentViewModel>> Payments { get; set; }
    }
}
