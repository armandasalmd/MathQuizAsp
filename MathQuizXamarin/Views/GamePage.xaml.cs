using MathQuizCore.Enums;
using MathQuizXamarin.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MathQuizXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        public GamePage(DifficultyLevel level, int questionCount)
        {
            InitializeComponent();
            BindingContext = new GameViewModel(this, level, questionCount);
            ButtonNext.IsEnabled = false;
        }

        public void SetButtonNextEnabled(bool enabled) => ButtonNext.IsEnabled = enabled;
        public void SetButtonCheckEnabled(bool enabled) => ButtonCheck.IsEnabled = enabled;
        public string GetUserGuess() => UserInput.Text;
        public void ClearUserInput()
        {
            UserInput.Text = string.Empty;
            UserInput.Focus();
        }
    }
}