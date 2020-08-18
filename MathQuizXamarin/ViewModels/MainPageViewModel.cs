using MathQuizCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MathQuizXamarin.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Game page with all elements
        /// </summary>
        private MainPage _view;
        public MainPage View { get => _view; }

        public List<string> DifficultyNames
        {
            get => Enum.GetNames(typeof(DifficultyLevel)).ToList();
        }

        private string _selectedDifficulty;
        public string SelectedDifficulty
        {
            get => _selectedDifficulty;
            set
            {
                _selectedDifficulty = value;
                OnPropertyChanged("SelectedDifficulty");
            }
        }

        private string _questionCount;
        public string QuestionCount
        {
            get => _questionCount;
            set
            {
                _questionCount = value;

                //OnPropertyChanged("QuestionCount");
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

        public ICommand StartGameCommand { get; } 

        public MainPageViewModel(MainPage View)
        {
            _selectedDifficulty = DifficultyNames[0];
            _view = View;

            StartGameCommand = new Command(HandleStartGame);
        }
         
        
        private bool StartGameValidation()
        {
            // Input validation
            bool isQCountValid = int.TryParse(QuestionCount, out int _);
            bool isDifficultyValid = DifficultyNames.Contains(SelectedDifficulty);
            if (!isQCountValid)
            {
                MessageToDisplay = "Invalid question count. Please fix it!";
                return false;
            }
            else if (!isDifficultyValid)
            {
                MessageToDisplay = "Invalid difficulty level!";
                return false;
            }
            else
            {
                MessageToDisplay = string.Empty;
                return true;
            }
        }

        private void HandleStartGame()
        {
            if (StartGameValidation())
            {
                // Start new activity here
                View.StartGamePage(
                    (DifficultyLevel)Enum.Parse(typeof(DifficultyLevel),
                    SelectedDifficulty), int.Parse(QuestionCount));
            }
        }
    }
}
