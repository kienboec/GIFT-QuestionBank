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
    public class PresentationViewModel : ViewModelBase
    {
        public PresentationViewModel(QuestionStore questionStore)
        {
            this._questionStore = questionStore;

            this.MessengerInstance.Register<ReadOnlyChangedMessage>(
                this,
                (message) =>
                {
                    IsReadonly = message.IsReadonly;
                    IsEnabled = !IsReadonly;
                });
        }

        public ObservableCollection<Question> Questions => _questionStore.Questions;
        private QuestionStore _questionStore;

        private bool _isReadonly;

        public bool IsReadonly
        {
            get => _isReadonly;
            set => Set(ref _isReadonly, value, nameof(IsReadonly));
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set => Set(ref _isEnabled, value, nameof(IsEnabled));
        }
    }
}
