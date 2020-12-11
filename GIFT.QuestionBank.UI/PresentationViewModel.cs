using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GIFT.QuestionBank.Shared.Model;

namespace GIFT.QuestionBank.UI
{
    public class PresentationViewModel : ViewModelBase
    {
        public PresentationViewModel(ObservableCollection<Question> questions)
        {
            this.Questions = questions;
        }

        public ObservableCollection<Question> Questions { get; }

    }
}
