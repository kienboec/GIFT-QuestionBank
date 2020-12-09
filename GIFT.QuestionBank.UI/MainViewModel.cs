using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GIFT.QuestionBank.Shared.Model;

namespace GIFT.QuestionBank.UI
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Question> Questions { get; }

        public RelayCommand ExitCommand { get; }
        public RelayCommand<Question> DeleteQuestionCommand { get; }

        public event EventHandler<EventArgs> RequestExit;

        public MainViewModel()
        {
            //if (this.IsInDesignMode)
            {
                Questions = new ObservableCollection<Question>();

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

            ExitCommand = new RelayCommand(() =>
                    {
                        RequestExit?.Invoke(this, EventArgs.Empty);
                    });
            DeleteQuestionCommand = new RelayCommand<Question>(
                (question) =>
                {
                    Questions.Remove(question);
                    DeleteQuestionCommand.RaiseCanExecuteChanged();
                },
                (question) => Questions.Count > 1);
        }
    }
}
