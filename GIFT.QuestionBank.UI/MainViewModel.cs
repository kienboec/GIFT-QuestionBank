using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GIFT.QuestionBank.UI.Model;

namespace GIFT.QuestionBank.UI
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Question> Questions { get; }

        public MainViewModel()
        {
            //if (this.IsInDesignMode)
            {
                Questions = new ObservableCollection<Question>();
                Questions.Add(new Question()
                {
                    QuestionName = "PeopleInGrantsTomb",
                    QuestionText = "What two people are entombed in Grant's tomb?",
                    Choices = new ObservableCollection<QuestionChoice>()
                    {
                        new QuestionChoice()
                        {
                            Percentage = -100,
                            Text = "No one",
                            Feedback = null
                        },
                        new QuestionChoice()
                        {
                            Percentage = 100,
                            Text = "Some one",
                            Feedback = "right... good choice"
                        }
                    }
                });

                Questions.Add(new Question()
                {
                    QuestionName = "name1",
                    QuestionText = "text1",
                    Choices = new ObservableCollection<QuestionChoice>()
                    {
                        new QuestionChoice()
                        {
                            Percentage = 100,
                            Text = "good",
                            Feedback = "right... good choice"
                        },
                        new QuestionChoice()
                        {
                            Percentage = -100,
                            Text = "bad",
                            Feedback = null
                        },
                    }
                });
            }
        }
    }
}
