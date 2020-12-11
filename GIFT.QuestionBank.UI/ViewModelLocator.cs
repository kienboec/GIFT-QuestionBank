using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;

namespace GIFT.QuestionBank.UI
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<QuestionStore>();
            SimpleIoc.Default.Register<NavigationService>();

            SimpleIoc.Default.Register<QuestionDetailViewModel>();
            SimpleIoc.Default.Register<PresentationViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main => 
            SimpleIoc.Default.GetInstance<MainViewModel>();
    }
}
