using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GIFT.QuestionBank.Shared.Model;
using GIFT.QuestionBank.Shared.Parser;

namespace GIFT.QuestionBank.UI
{
    public class MainViewModel : ViewModelBase
    {
        private QuestionStore _questionStore;
        private NavigationService _navigationService;
        public ObservableCollection<Question> Questions
            => _questionStore.Questions;

        public RelayCommand ExitCommand { get; }
        public RelayCommand LoadDataCommand { get; }
        public RelayCommand<Question> DeleteQuestionCommand { get; }
        public RelayCommand<Question> PresentQuestionCommand { get; }
        public RelayCommand<Question> EditQuestionCommand { get; }

        public event EventHandler<EventArgs> RequestExit;

        public ViewModelBase DetailVM
        {
            get => _detailVm;
            set => Set(ref _detailVm, value, nameof(DetailVM));
        }

        public bool IsTimerEnabled
        {
            get => _dispatcherTimer.IsEnabled;
            set
            {
                _dispatcherTimer.IsEnabled = value;
                RaisePropertyChanged();
            }
        }

        private DispatcherTimer _dispatcherTimer;
        private int _remainingSeconds;
        private ViewModelBase _detailVm;
        public const int IntervalToLoadInSec = 10;

        public int RemainingSeconds
        {
            get => _remainingSeconds;
            set
            {
                this.Set(ref _remainingSeconds, value, nameof(RemainingSeconds));
            }
        }

        public MainViewModel(QuestionStore questionStore, NavigationService navigationService)
        {
            _questionStore = questionStore;
            _navigationService = navigationService;

            RemainingSeconds = IntervalToLoadInSec;
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Tick += (sender, e) =>
            {
                RemainingSeconds = RemainingSeconds - 1;
                if (RemainingSeconds <= 0)
                {
                    LoadDataCommand.Execute(null);
                    RemainingSeconds = IntervalToLoadInSec;
                }
            };
            _dispatcherTimer.Start();
            IsTimerEnabled = false;

            this.DetailVM = this._navigationService.GetViewModel(NavigationService.Detail);

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
            PresentQuestionCommand = new RelayCommand<Question>(
                (question) =>
                {
                    DetailVM = this._navigationService.GetViewModel(NavigationService.Presentation);
                });
            EditQuestionCommand = new RelayCommand<Question>(
                (question) =>
                {
                    DetailVM = this._navigationService.GetViewModel(NavigationService.Detail);
                });
        }

        private async Task LoadData()
        {
            List<Question> questions = new List<Question>();



            await Task.Run(async () =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    // reset
                    while (this.Questions.Count > 0)
                    {
                        this.Questions.RemoveAt(0);
                    }
                });

                using TcpClient client = new TcpClient("localhost", 8000);
                using var reader = new StreamReader(client.GetStream());
                using var writer = new StreamWriter(client.GetStream()) { AutoFlush = true };

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



            foreach (var question in questions)
            {
                this.Questions.Add(question);
            }
        }
    }
}
