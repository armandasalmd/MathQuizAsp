using MathQuizXamarin.Models;
using MathQuizXamarin.Views;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using MathQuizCore.Interfaces;
using MathQuizCore.Enums;
using MathQuizXamarin.Services;

namespace MathQuizXamarin.ViewModels
{
    public interface IGameCommands
    {
        ICommand CheckAnswerCommand { get; }
        ICommand NextQuestionCommand { get; }
    }

    public class GameViewModel : BaseViewModel, IGameCommands
    {
        /// <summary>
        /// Game page with all elements
        /// </summary>
        private GamePage _view;
        public GamePage View { get => _view; }

        /// <summary>
        /// Question history list
        /// </summary>
        public List<IQuestion<int>> QuestionHistory;

        /// <summary>
        /// Current question that is displayed
        /// </summary>
        private IQuestion<int> _currectQuestion;
        public IQuestion<int> CurrentQuestion
        {
            get { return _currectQuestion; }
            set
            {
                _currectQuestion = value;
                QuestionToDisplay = value.GetQuestion();
                OnPropertyChanged("QuestionToDisplay");
            }
        }

        public DifficultyLevel GameDifficulty { get; set; }

        public ICommand CheckAnswerCommand { get; }
        public ICommand NextQuestionCommand { get; }

        /// <summary>
        /// Game statistics
        /// </summary>
        private GameStatistics _gameStatistic;
        public GameStatistics Statistics
        {
            get
            {
                if (_gameStatistic == null)
                    return new GameStatistics();
                return _gameStatistic;
            }
            set { _gameStatistic = value; }
        }

        public string StatisticsToDisplay
        {
            get => $"{Statistics.CorrectAnswers} / {Statistics.TotalAnswers} ({Statistics.CorrectPercent}%)";
        }

        /// <summary>
        /// Question that is binded to the view
        /// </summary>
        private string _questionToDisplay;
        public string QuestionToDisplay
        {
            get { return _questionToDisplay; }
            set
            {
                _questionToDisplay = value;
                OnPropertyChanged("QuestionToDisplay");
            }
        }

        /// <summary>
        /// Answer that is displayed on the screen
        /// </summary>
        private string _answerToDisplay;
        public string AnswerToDisplay
        {
            get { return _answerToDisplay; }
            set
            {
                _answerToDisplay = value;
                OnPropertyChanged("AnswerToDisplay");
                OnPropertyChanged("IsQuestionAnswered");
            }
        }
        public bool IsQuestionAnswered
        {
            get => (AnswerToDisplay != null && AnswerToDisplay.Length > 0);
        }
        public bool IsQuestionNotAnswered
        {
            get => !IsQuestionAnswered;
        }

        private bool _isCheckButtonEnabled;
        public bool IsCheckButtonEnabled 
        { 
            get => _isCheckButtonEnabled;
            set 
            {
                _isCheckButtonEnabled = value;
                OnPropertyChanged("IsCheckButtonEnabled");
            }
        }

        private bool _isNextButtonEnabled;
        public bool IsNextButtonEnabled
        {
            get => _isNextButtonEnabled;
            set
            {
                _isNextButtonEnabled = value;
                OnPropertyChanged("IsNextButtonEnabled");
            }
        }

        /// <summary>
        /// Error message to be displayed for the user
        /// </summary>
        private string _messageToDisplay;
        public string MessageToDisplay
        {
            get { return _messageToDisplay; }
            set
            {
                _messageToDisplay = value;
                OnPropertyChanged("MessageToDisplay");
                OnPropertyChanged("IsMessage");
            }
        }
        public bool IsMessage { get => (MessageToDisplay != null && MessageToDisplay.Length > 0); }
        private MessageType _messageType;
        public MessageType MyMessageType
        {
            get
            {
                return _messageType;
            }
            set
            {
                _messageType = value;
                OnPropertyChanged("MyMessageType");
            }
        }

        public GameViewModel(GamePage gameView, DifficultyLevel level, int questionCount)
        {
            _view = gameView;
            IsCheckButtonEnabled = true;
            IsNextButtonEnabled = false;
            CheckAnswerCommand = new Command(HandleCheckAnswer);
            NextQuestionCommand = new Command(HandleNextQuestion);

            AnswerToDisplay = string.Empty; // clear answer
            MessageToDisplay = string.Empty; // clear error
            GameDifficulty = level;
            QuestionHistory = new List<IQuestion<int>>();
            CreateAndSetNewQuestion();
        }

        private void HandleCheckAnswer()
        {
            string userGuess = View.GetUserGuess();
            if (userGuess != string.Empty && userGuess != null)
            {
                string realAnswer = CurrentQuestion.Answer.ToString();
                AnswerToDisplay = realAnswer;
                if (realAnswer == userGuess)
                {
                    MessageToDisplay = "You are correct!";
                    MyMessageType = MessageType.Success;
                    Statistics.CorrectAnswers++;
                }
                else
                {
                    MessageToDisplay = "You are incorrect!";
                    MyMessageType = MessageType.Error;
                }
                Statistics.TotalAnswers++;

                View.SetButtonCheckEnabled(false);
                View.SetButtonNextEnabled(true);
                //IsCheckButtonEnabled = false;
                //IsNextButtonEnabled = true;
            }
            else
            {
                MessageToDisplay = "Please enter your answer!";
                MyMessageType = MessageType.Warning;
            }
        }

        private void HandleNextQuestion()
        {

            if (CurrentQuestion != null)
            {
                QuestionHistory?.Add(CurrentQuestion);
            }
            CreateAndSetNewQuestion();

            AnswerToDisplay = string.Empty; // clear answer
            MessageToDisplay = string.Empty; // clear error

            View.SetButtonCheckEnabled(true);
            View.SetButtonNextEnabled(false);
            //IsCheckButtonEnabled = true;
            //IsNextButtonEnabled = false;
            View.ClearUserInput();
        }

        private void CreateAndSetNewQuestion()
        {
            CurrentQuestion = new MathQuestion(GameDifficulty);
        }

    }
}
