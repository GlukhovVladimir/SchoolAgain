using System;
using System.Collections.Generic;
using System.Text;
using SchoolAgainBusinessLogic.ViewModels;

namespace SchoolAgainBusinessLogic.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ServiceViewModel> Services { get; set; }
    }
}
