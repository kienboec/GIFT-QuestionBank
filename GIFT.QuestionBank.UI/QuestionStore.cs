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
    public class QuestionStore
    {
        public ObservableCollection<Question> Questions { get; set; } 
            = new ObservableCollection<Question>();

        public QuestionStore()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {

                {
                    var question = new Question()
                    {
                        QuestionName = "PeopleInGrantsTomb",
                        QuestionText = "What two people are entombed in Grant's tomb?"
                    };

                    question.Choices.Add(new QuestionChoice()
                    {
                        Percentage = -100,
                        Text = "No one",
                        Feedback = null
                    });

                    question.Choices.Add(
                        new QuestionChoice()
                        {
                            Percentage = 100,
                            Text = "Some one",
                            Feedback = "right... good choice"
                        });

                    Questions.Add(question);
                }

                {
                    var question = new Question()
                    {
                        QuestionName = "name1",
                        QuestionText = "text1",
                    };

                    question.Choices.Add(
                        new QuestionChoice()
                        {
                            Percentage = 100,
                            Text = "good",
                            Feedback = "right... good choice"
                        });

                    question.Choices.Add(
                        new QuestionChoice()
                        {
                            Percentage = -100,
                            Text = "bad",
                            Feedback = null
                        });

                    Questions.Add(question);
                }
            }
        }
    }
}
