using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIFT.QuestionBank.Shared.Model
{
    public class Question
    {
        public string QuestionName { get; set; }
        public string QuestionText { get; set; }
        public ObservableCollection<QuestionChoice> Choices { get; } = new ObservableCollection<QuestionChoice>();
    }
}
