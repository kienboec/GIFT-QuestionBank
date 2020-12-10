using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GIFT.QuestionBank.Shared.Model;
using GIFT.QuestionBank.Shared.Parser;

namespace GIFT.QuestionBank.UI
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Question> Questions { get; }

        public RelayCommand ExitCommand { get; }
        public RelayCommand LoadDataCommand { get; }
        public RelayCommand<Question> DeleteQuestionCommand { get; }

        public event EventHandler<EventArgs> RequestExit;

        public MainViewModel()
        {
            Questions = new ObservableCollection<Question>();

            if (this.IsInDesignMode)
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

            LoadDataCommand = new RelayCommand(async () =>
            {
                await LoadData();
            });
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

        private async Task LoadData()
        {
            List<Question> questions = new List<Question>();

            

            await Task.Run(async () =>
            {

                using TcpClient client = new TcpClient("localhost", 8000);
                using var reader = new StreamReader(client.GetStream());
                using var writer = new StreamWriter(client.GetStream()) {AutoFlush = true};

                List<string> questionNames = new List<string>();
                string line;

                await writer.WriteLineAsync("LIST");

                // add item line by line
                while (!string.IsNullOrWhiteSpace(line = await reader.ReadLineAsync()))
                {
                    questionNames.Add(line);
                }

                foreach (var questionName in questionNames)
                {
                    await writer.WriteLineAsync("GET");
                    await writer.WriteLineAsync(questionName);

                    StringBuilder giftQuestion = new StringBuilder();
                    while (!string.IsNullOrWhiteSpace(line = await reader.ReadLineAsync()))
                    {
                        giftQuestion.AppendLine(line);
                    }

                    GIFTParser parser = new GIFTParser();
                    var questionsOfGift = parser.Parse(giftQuestion.ToString());
                    questions.AddRange(questionsOfGift);
                }

                await writer.WriteLineAsync("QUIT");
            });

            // reset
            while (this.Questions.Count > 0)
            {
                this.Questions.RemoveAt(0);
            }

            foreach (var question in questions)
            {
                this.Questions.Add(question);
            }
        }
    }
}
