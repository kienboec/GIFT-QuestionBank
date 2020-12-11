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
        public PresentationViewModel(QuestionStore questionStore)
        {
            this._questionStore = questionStore;
        }

        public ObservableCollection<Question> Questions => _questionStore.Questions;
        private QuestionStore _questionStore;
    }
}
