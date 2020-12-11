using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace GIFT.QuestionBank.UI
{
    public class NavigationService
    {
        public const string Presentation = "present";
        public const string Detail = "detail";

        public ViewModelBase GetViewModel(string vmName)
        {
            return vmName switch
            {
                "detail" => SimpleIoc.Default.GetInstance<QuestionDetailViewModel>(),
                "present" => SimpleIoc.Default.GetInstance<PresentationViewModel>(),
                _ => null
            };
        }
    }
}
