using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GIFT.QuestionBank.Shared.Model;
using GIFT.QuestionBank.UI.Message;

namespace GIFT.QuestionBank.UI
{
    public class QuestionDetailViewModel : ViewModelBase
    {
        public QuestionDetailViewModel(QuestionStore questionStore)
        {
            this._questionStore = questionStore;
            this.MessengerInstance.Register<ReadOnlyChangedMessage>(
                this,
                (message) =>
                {
                    IsReadonly = message.IsReadonly;
                });
        }

        public ObservableCollection<Question> Questions => this._questionStore.Questions;

        private QuestionStore _questionStore;
        private bool _isReadonly;

        public bool IsReadonly
        {
            get => _isReadonly;
            set => Set(ref _isReadonly, value, nameof(IsReadonly));
        }
    }
}
